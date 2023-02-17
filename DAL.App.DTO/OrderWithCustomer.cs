using System.ComponentModel;

namespace DAL.App.DTO;

public class OrderWithCustomer
{
    public required Guid Id { get; set; }

    [DisplayName("Route name")]
    public required string RouteName { get; set; } = default!;
    
    public required double Price { get; set; }
    
    public required DateTime FlightStart { get; set; }
    public required DateTime FlightEnd { get; set; }
    
    [DisplayName("Company name")]
    public required string CompanyName { get; set; } = default!;
    public required DateTime DateOfPurchase { get; set; }

    public required Guid CustomerId { get; set; }
    
    [DisplayName("First name")]
    public required string CustomerFirstName { get; set; } = default!;
    
    [DisplayName("Last name")]
    public required string CustomerLastName { get; set; } = default!;
}