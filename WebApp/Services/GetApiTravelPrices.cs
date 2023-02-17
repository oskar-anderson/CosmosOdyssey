using System.Globalization;
using System.Timers;
using DAL.App.EF;
using ServiceDTO.CosmosOdysseyApi;
using System.Net.Http.Json;

namespace WebApp.Services;

public class GetApiTravelPrices : IGetApiTravelPrices
{
    private readonly System.Timers.Timer _timer;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<GetApiTravelPrices> _logger;

    public GetApiTravelPrices(ILogger<GetApiTravelPrices> logger, IServiceScopeFactory scopeFactory, IHttpClientFactory httpClientFactory)
    {
        _timer = new System.Timers.Timer();
        _scopeFactory = scopeFactory;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    public async void Start(string url)
    {
        _logger.LogInformation(url);
        _timer.Elapsed += async (object? sender, ElapsedEventArgs elapsedEventArgs) =>
        {
            _logger.LogInformation($"{nameof(UpdateLoop)}");
            await UpdateLoop(url);
        };
        _logger.LogInformation($"{nameof(UpdateLoop)}");
        await UpdateLoop(url);
        _timer.Start();
    }


    /// <summary>
    /// Main method for timer to update PriceList data from API.
    /// Will also set the correct timer interval for future query.
    /// </summary>
    public async Task UpdateLoop(string url)
    {
        var scope = _scopeFactory.CreateScope();
        var databaseContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // last PriceList is still valid check
        var lastItem = databaseContext.PriceList.OrderByDescending(p => p.ValidUntil).FirstOrDefault();
        if (lastItem != null && lastItem.ValidUntil > DateTime.UtcNow)
        {
            _logger.LogInformation($"LastItem is still valid. Interval: {(lastItem.ValidUntil - DateTime.UtcNow).TotalMilliseconds}");
            _timer.Interval = (lastItem.ValidUntil - DateTime.UtcNow).TotalMilliseconds;
            return; // last item is still valid, do not update anything
        }

        var apiPriceList = await GetPriceListAsync(url);
        if (apiPriceList == null || apiPriceList.ValidUntil < DateTime.UtcNow)
            _timer.Interval = 60 * 1000;  // something went wrong, start updating every 60 seconds
        else
            _timer.Interval = (apiPriceList.ValidUntil - DateTime.UtcNow).TotalMilliseconds; // next update should be when previous expires
        _logger.LogInformation($"Next update in {_timer.Interval.ToString(CultureInfo.InvariantCulture)} milliseconds.");
        if (apiPriceList == null) return;
        _logger.LogInformation($"Data will be invalid at: {apiPriceList.ValidUntil.ToString(CultureInfo.InvariantCulture)}.");

        // add ProvidedRoute[] and PriceList data to transaction
        var priceListId = Guid.NewGuid();
        var providedRouteList = await GetProvidedRoutes(apiPriceList, databaseContext, priceListId);
        var providedRouteRepository = new DAL.App.EF.Repositories.ProvidedRouteRepository(databaseContext);
        providedRouteRepository.RemoveAll();
        foreach (var providedRoute in providedRouteList)
        {
            await providedRouteRepository.Add(providedRoute);
        }
        var priceList = new DAL.App.DTO.PriceList()
        {
            Id = priceListId,  // maybe should be using apiPriceList.Id instead
            ValidUntil = apiPriceList.ValidUntil,
            ValueJson = System.Text.Json.JsonSerializer.Serialize(apiPriceList)
        };
        var priceRepository = new DAL.App.EF.Repositories.PriceListRepository(databaseContext);
        await priceRepository.Add(priceList);
        
        // remove old PriceList
        // this will also delete the providedRoute by cascade delete
        await priceRepository.OrderByValidUntilDescendingThenSkipNThenDeleteAll(15);

        // commit transaction
        await databaseContext.SaveChangesAsync();
    }

    public async Task<ServiceDTO.CosmosOdysseyApi.ApiPriceList?> GetPriceListAsync(string apiUrl)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(apiUrl);
            if (! response.IsSuccessStatusCode)
            {
                _logger.LogCritical($"API request failed: ${response.ReasonPhrase}");
                return null;
            }
            var apiPriceList = await response.Content.ReadFromJsonAsync<ServiceDTO.CosmosOdysseyApi.ApiPriceList>();
            return apiPriceList;
        }
        catch (Exception ex)
        {
            _logger.LogCritical($"API request failed: ${ex.Message}");
            return null;
        }
    }

    public async Task<List<DAL.App.DTO.ProvidedRouteNavigationless>> GetProvidedRoutes(ApiPriceList apiPriceList, AppDbContext dbContext, Guid priceListId)
    {
        var ourCompanies = await new DAL.App.EF.Repositories.CompanyRepository(dbContext).GetAllAsyncBase();
        var ourLocations = await new DAL.App.EF.Repositories.LocationRepository(dbContext).GetAllAsyncBase();
        var ourLocationNames = ourLocations.Select(x => x.PlanetName).ToList();
        var providedRouteList = new List<DAL.App.DTO.ProvidedRouteNavigationless>();
        foreach (var leg in apiPriceList.Legs)
        {
            if (! ourLocationNames.Contains(leg.routeInfo.From.Name) || ! ourLocationNames.Contains(leg.routeInfo.To.Name))  // Unknown location, will be ignored
            {
                _logger.LogWarning($"Skipping unknown planet {leg.routeInfo.From.Name} or {leg.routeInfo.To.Name}");
                continue;
            }

            var ourFromLocation = ourLocations.FirstOrDefault(x => x.PlanetName == leg.routeInfo.From.Name)!;
            var ourToLocation = ourLocations.FirstOrDefault(x => x.PlanetName == leg.routeInfo.To.Name)!;
            foreach (var provider in leg.Providers)
            {
                var ourCompany = ourCompanies.FirstOrDefault(x => x.Name == provider.Company.Name);
                if (ourCompany == null)  // Unknown company, will be ignored
                {
                    _logger.LogWarning($"Skipping unknown company {provider.Company.Name}.");
                    continue;
                }
                var providedRoute = new DAL.App.DTO.ProvidedRouteNavigationless()
                {
                    Id = Guid.NewGuid(),
                    PriceListId = priceListId,
                    CompanyId = ourCompany.Id,
                    DestinationLocationId = ourToLocation.Id,
                    FromLocationId = ourFromLocation.Id,
                    Distance = leg.routeInfo.Distance,
                    FlightStart = provider.FlightStart,
                    FlightEnd = provider.FlightEnd,
                    Price = provider.Price,
                };
                providedRouteList.Add(providedRoute);
            }
        }

        return providedRouteList;
    }


}