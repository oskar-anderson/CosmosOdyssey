using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.App;

public class Order : DomainEntityMetadata
{
    public required string RouteName { get; set; } = default!;
    
    [Precision(28, 2)]
    public required double Price { get; set; }
    
    public required DateTime FlightStart { get; set; }
    public required DateTime FlightEnd { get; set; }

    public required string CompanyName { get; set; } = default!;
    public required DateTime DateOfPurchase { get; set; }

    public required Guid CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
}