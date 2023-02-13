using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class Order : DomainEntityMetadata
{
    [MaxLength(255)]
    public string FirstName { get; set; } = default!;

    [MaxLength(255)]
    public string LastName { get; set; } = default!;
    
    public DateTime DateOfPurchase { get; set; } = default;

    public ICollection<OrderLine> OrderLines { get; set; } = default!;
}