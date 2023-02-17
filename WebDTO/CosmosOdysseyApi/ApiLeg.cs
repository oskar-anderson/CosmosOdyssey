namespace WebDTO.CosmosOdysseyApi;

public class ApiLeg
{
    public required string Id { get; set; }
    public required ApiRouteInfo routeInfo { get; set; }
    public required List<ApiProvider> Providers { get; set; }
}