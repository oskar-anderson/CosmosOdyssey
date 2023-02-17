namespace WebDTO.CosmosOdysseyApi;

public class ApiProvider
{
    public required string Id { get; set; }
    public required ApiCompany Company { get; set; }
    public required double Price { get; set; }
    public required DateTime FlightStart { get; set; }
    public required DateTime FlightEnd { get; set; }
}