namespace ServiceDTO.CosmosOdysseyApi;

public class ApiLeg
{
    public string Id { get; set; }
    public ApiRouteInfo routeInfo { get; set; }
    public List<ApiProvider> Providers { get; set; }
}