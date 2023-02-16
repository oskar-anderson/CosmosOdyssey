namespace ServiceDTO.CosmosOdysseyApi;

public class ApiPriceList
{
    // This will be mapped from API, so names should match the API naming case insensitively.
    
    public required string Id { get; set; }
    public required DateTime ValidUntil { get; set; }
    public required List<ApiLeg> Legs { get; set; }
}