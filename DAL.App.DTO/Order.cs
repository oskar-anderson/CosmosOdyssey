namespace DAL.App.DTO;

public class Order
{
    public required Guid Id { get; set; }

    public required string RouteName { get; set; } = default!;
    
    public required double Price { get; set; }
    
    public required DateTime FlightStart { get; set; }
    public required DateTime FlightEnd { get; set; }
    
    public required string CompanyName { get; set; } = default!;
    public required DateTime DateOfPurchase { get; set; }

    public required Guid CustomerId { get; set; }
}