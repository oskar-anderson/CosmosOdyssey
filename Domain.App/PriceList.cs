using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App;

public class PriceList : DomainEntityMetadata
{
    public int Counter { get; set; }
    public DateTime ValidUntil { get; set; }
    
    [Column("nvarchar(max)")]  // this should be default datatype value for string, but annotation seems more readable
    public string ValueJson { get; set; } = default!;
}