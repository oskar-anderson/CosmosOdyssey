using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.App;

public class ProvidedRoute : DomainEntityMetadata
{
    [ForeignKey(nameof(Location))]
    public Guid FromLocationId { get; set; }
    public Location FromLocation { get; set; } = default!;
    
    [ForeignKey(nameof(Location))]
    public Guid DestinationLocationId { get; set; }
    public Location DestinationLocation { get; set; } = default!;
    
    public long Distance { get; set; }
    
    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = default!;
    
    [Precision(28, 2)]
    public double Price { get; set; }
    
    public DateTime FlightStart { get; set; }
    public DateTime FlightEnd { get; set; }
}