using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class Customer : DomainEntityMetadata
{
    [MaxLength(255)]
    public required string FirstName { get; set; } = default!;

    [MaxLength(255)]
    public required string LastName { get; set; } = default!;
    
    public virtual ICollection<Order>? Orders { get; set; }
}