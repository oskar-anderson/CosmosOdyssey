namespace ServiceDTO.CosmosOdysseyApi;

public class ApiRouteInfo
{
    public required string Id { get; set; }
    public required ApiPlanet From { get; set; }
    public required ApiPlanet To { get; set; }
    public required long Distance { get; set; } 
}