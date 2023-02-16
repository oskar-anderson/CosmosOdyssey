using Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.App;

public class OrderLine : DomainEntityMetadata
{
    public Guid OrderId { get; set; }
    public virtual Order? Order { get; set; }
    // From->Destination
    public string RouteName { get; set; } = default!;
    
    [Precision(28, 2)]
    public double Price { get; set; }
    
    public DateTime FlightStart { get; set; }
    public DateTime FlightEnd { get; set; }
    
    public string CompanyName { get; set; } = default!;
}