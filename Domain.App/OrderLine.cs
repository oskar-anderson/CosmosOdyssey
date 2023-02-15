using Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.App;

public class OrderLine : DomainEntityMetadata
{
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
    
    public decimal QuotedPrice { get; set; }
    // From->Destination
    public string RouteName { get; set; } = default!;
    
    [Precision(28, 2)]
    public decimal Price { get; set; }
    
    public DateTime FlightStart { get; set; }
    public DateTime FlightEnd { get; set; }
    
    public string CompanyName { get; set; } = default!;
}