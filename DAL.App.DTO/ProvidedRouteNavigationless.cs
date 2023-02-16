namespace DAL.App.DTO;

public class ProvidedRouteNavigationless
{
    public required Guid Id { get; set; }
    public required Guid PriceListId { get; set; }
    
    public required Guid FromLocationId { get; set; }

    public required Guid DestinationLocationId { get; set; }

    public required long Distance { get; set; }
    
    public required Guid CompanyId { get; set; }

    public required double Price { get; set; }
    
    public required DateTime FlightStart { get; set; }
    public required DateTime FlightEnd { get; set; }
}