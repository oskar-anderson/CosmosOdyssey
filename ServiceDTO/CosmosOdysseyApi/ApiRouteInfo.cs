namespace ServiceDTO.CosmosOdysseyApi;

public class ApiRouteInfo
{
    public string Id { get; set; }
    public ApiPlanet From { get; set; }
    public ApiPlanet To { get; set; }
    public long Distance { get; set; } 
}