using WebDTO.CosmosOdysseyApi;

namespace WebApp.Services;

public interface IGetApiTravelPrices
{
    void Start(string url);
    public Task<ApiPriceList?> GetPriceListAsync(string apiUrl);
}