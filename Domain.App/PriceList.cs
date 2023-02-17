using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App;

public class PriceList : DomainEntityMetadata
{
    public required DateTime ValidUntil { get; set; }
    
    public required string ValueJson { get; set; } = default!;

    public virtual ICollection<ProvidedRoute>? ProvidedRoutes { get; set; }
}