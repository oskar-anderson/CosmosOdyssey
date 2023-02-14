namespace ServiceDTO.CosmosOdysseyApi;

public class ApiPriceList
{
    public string Id { get; set; }
    public DateTime ValidUntil { get; set; }
    public List<ApiLeg> Legs { get; set; } 
}