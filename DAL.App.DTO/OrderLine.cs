namespace DAL.App.DTO;

public class OrderLine
{
    public required Guid Id { get; set; }
    
    public required Guid OrderId { get; set; }
    // No parent navigational property to prevent circular navigational
    // public Order Order { get; set; } = default!;
    
    // From->Destination
    public required string RouteName { get; set; } = default!;
    
    public required double Price { get; set; }
    
    public required DateTime FlightStart { get; set; }
    public required DateTime FlightEnd { get; set; }
    
    public required string CompanyName { get; set; } = default!;
}