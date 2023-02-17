using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.App;

public class ProvidedRoute : DomainEntityMetadata
{
    [ForeignKey(nameof(PriceList))]
    public required Guid PriceListId { get; set; }
    
    public virtual PriceList? PriceList { get; set; }

    [ForeignKey(nameof(FromLocation))]
    public required Guid FromLocationId { get; set; }
    public virtual Location? FromLocation { get; set; }
    
    [ForeignKey(nameof(DestinationLocation))]
    public required Guid DestinationLocationId { get; set; }
    public virtual Location? DestinationLocation { get; set; }
    
    public required long Distance { get; set; }
    
    [ForeignKey(nameof(Company))]
    public required Guid CompanyId { get; set; }
    public virtual Company? Company { get; set; }
    
    [Precision(28, 2)]
    public required double Price { get; set; }
    
    public required DateTime FlightStart { get; set; }
    public required DateTime FlightEnd { get; set; }
}