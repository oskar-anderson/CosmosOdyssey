namespace ServiceDTO.CosmosOdysseyApi;

public class ApiProvider
{
    public string Id { get; set; }
    public ApiCompany Company { get; set; }
    public double Price { get; set; }
    public DateTime FlightStart { get; set; }
    public DateTime FlightEnd { get; set; }
}