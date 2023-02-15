using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App;

public class PriceList : DomainEntityMetadata
{
    public DateTime ValidUntil { get; set; }
    
    public string ValueJson { get; set; } = default!;
}