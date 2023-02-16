using System.ComponentModel;

namespace DAL.App.DTO;

public class ProvidedRoute
{
    public required Guid Id { get; set; }
    
    public required Guid PriceListId { get; set; }
    
    public required Location FromLocation { get; set; }
    
    public required Location DestinationLocation { get; set; }
    
    public required long Distance { get; set; }
    
    public required Company Company { get; set; }
    
    public required double Price { get; set; }
    
    [DisplayName("Flight start")]
    public required DateTime FlightStart { get; set; }
    [DisplayName("Flight end")]
    public required DateTime FlightEnd { get; set; }
}