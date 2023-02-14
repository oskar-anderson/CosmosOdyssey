using System.Timers;
using DAL.App.EF;
using ServiceDTO.CosmosOdysseyApi;

namespace WebApp.Services;

public class GetApiData
{
    private readonly System.Timers.Timer _timer;
    private readonly AppDbContext _dbContext;
    private readonly HttpClient _httpClient;
    private readonly string _url;

    public GetApiData(AppDbContext dbContext, HttpClient httpClient, string url)
    {
        _timer = new System.Timers.Timer();
        _dbContext = dbContext;
        _httpClient = httpClient;
        _url = url;
    }
    
    public void Start()
    {
        _timer.Elapsed += (object? sender, ElapsedEventArgs elapsedEventArgs) => UpdateLoop();
        UpdateLoop();
        _timer.Start();
    }


    /// <summary>
    /// Main method for timer to update PriceList data from API.
    /// Will also set the correct timer interval for future query.
    /// </summary>
    public async void UpdateLoop()
    {
        // last PriceList is still valid check
        var lastItem = _dbContext.PriceList.OrderByDescending(p => p.ValidUntil).FirstOrDefault();
        if (lastItem != null || lastItem!.ValidUntil > DateTime.Now) { return; }

        var apiPriceList = await GetPriceListAsync(_url);
        if (apiPriceList == null || apiPriceList.ValidUntil < DateTime.Now)
            _timer.Interval = 60 * 1000;  // something went wrong, start updating every 60 seconds
        else
            _timer.Interval = (apiPriceList.ValidUntil - DateTime.Now).Milliseconds; // next update should be when previous expires
        if (apiPriceList == null) return;

        // add ProvidedRoute[] and PriceList data to transaction
        var providedRouteList = await GetProvidedRoutes(apiPriceList);
        var providedRouteRepository = new DAL.App.EF.Repositories.ProvidedRouteRepository(_dbContext);
        foreach (var providedRoute in providedRouteList)
        {
            await providedRouteRepository.Add(providedRoute);
        }
        var priceList = new DAL.App.DTO.PriceList()
        {
            Id = Guid.NewGuid(),  // maybe should be using apiPriceList.Id instead
            ValidUntil = apiPriceList.ValidUntil,
            ValueJson = System.Text.Json.JsonSerializer.Serialize(apiPriceList.Legs)
        };
        var priceRepository = new DAL.App.EF.Repositories.PriceListRepository(_dbContext);
        await priceRepository.Add(priceList);
        
        // remove old PriceList
        await priceRepository.OrderByValidUntilDescendingThenDeleteLastNElements(15);
        
        // commit transaction
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ServiceDTO.CosmosOdysseyApi.ApiPriceList?> GetPriceListAsync(string apiUrl)
    {
        try
        {
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            var rawResponse = await response.Content.ReadAsStringAsync();
            var apiPriceList = System.Text.Json.JsonSerializer.Deserialize<ServiceDTO.CosmosOdysseyApi.ApiPriceList>(rawResponse);
            return apiPriceList;
        }
        catch (Exception ex) when (ex is InvalidOperationException ||
                                   ex is HttpRequestException ||
                                   ex is TaskCanceledException ||
                                   ex is UriFormatException)
        {
            return null;
        }
    }

    public async Task<List<DAL.App.DTO.ProvidedRoute>> GetProvidedRoutes(ApiPriceList apiPriceList)
    {
        var ourCompanies = await new DAL.App.EF.Repositories.CompanyRepository(_dbContext).GetAllAsyncBase();
        var ourLocations = await new DAL.App.EF.Repositories.LocationRepository(_dbContext).GetAllAsyncBase();
        var ourLocationNames = ourLocations.Select(x => x.PlanetName).ToList();
        var providedRouteList = new List<DAL.App.DTO.ProvidedRoute>();
        foreach (var leg in apiPriceList.Legs)
        {
            if (! ourLocationNames.Contains(leg.routeInfo.From.Name) || ! ourLocationNames.Contains(leg.routeInfo.To.Name))  // Unknown location, will be ignored
            {
                continue;
            }

            var ourFromLocation = ourLocations.FirstOrDefault(x => x.PlanetName == leg.routeInfo.From.Name)!;
            var ourToLocation = ourLocations.FirstOrDefault(x => x.PlanetName == leg.routeInfo.To.Name)!;
            foreach (var provider in leg.Providers)
            {
                var ourCompany = ourCompanies.FirstOrDefault(x => x.Name == provider.Company.Name);
                if (ourCompany == null)  // Unknown company, will be ignored
                {
                    continue;
                }
                var providedRoute = new DAL.App.DTO.ProvidedRoute()
                {
                    Id = Guid.NewGuid(),
                    CompanyId = ourCompany.Id,
                    Company = ourCompany,
                    DestinationLocationId = ourToLocation.Id,
                    DestinationLocation = ourToLocation,
                    FromLocationId = ourFromLocation.Id,
                    FromLocation = ourFromLocation,
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