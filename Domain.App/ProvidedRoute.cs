using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.App;

public class ProvidedRoute : DomainEntityMetadata
{
    [ForeignKey(nameof(PriceList))]
    public Guid PriceListId { get; set; }
    
    public virtual PriceList? PriceList { get; set; }

    [ForeignKey(nameof(FromLocation))]
    public Guid FromLocationId { get; set; }
    public virtual Location? FromLocation { get; set; }
    
    [ForeignKey(nameof(DestinationLocation))]
    public Guid DestinationLocationId { get; set; }
    public virtual Location? DestinationLocation { get; set; }
    
    public long Distance { get; set; }
    
    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; set; }
    public virtual Company? Company { get; set; }
    
    [Precision(28, 2)]
    public double Price { get; set; }
    
    public DateTime FlightStart { get; set; }
    public DateTime FlightEnd { get; set; }
}