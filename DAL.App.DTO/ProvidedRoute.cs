namespace DAL.App.DTO;

public class ProvidedRoute
{
    public required Guid Id { get; set; }
    
    public required Guid FromLocationId { get; set; }
    public Location? FromLocation { get; set; }
    
    public required Guid DestinationLocationId { get; set; }
    public Location? DestinationLocation { get; set; }
    
    public required long Distance { get; set; }
    
    public required Guid CompanyId { get; set; }
    public Company? Company { get; set; }
    
    public required double Price { get; set; }
    
    public required DateTime FlightStart { get; set; }
    public required DateTime FlightEnd { get; set; }
}