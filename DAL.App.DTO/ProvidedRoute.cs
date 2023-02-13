namespace DAL.App.DTO;

public class ProvidedRoute
{
    public required Guid Id { get; set; }
    
    public required Guid FromLocationId { get; set; }
    public required Location FromLocation { get; set; } = default!;
    
    public required Guid DestinationLocationId { get; set; }
    public required Location DestinationLocation { get; set; } = default!;
    
    public required Guid CompanyId { get; set; }
    public required Company Company { get; set; } = default!;
    
    public required decimal Price { get; set; }
    
    public required DateTime FlightStart { get; set; }
    public required DateTime FlightEnd { get; set; }
}