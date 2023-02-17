using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO;

public class Customer
{
    public required Guid Id { get; set; }
    
    [MaxLength(255)]
    public required string FirstName { get; set; } = default!;

    [MaxLength(255)]
    public required string LastName { get; set; } = default!;
    
    public required ICollection<Order> Orders { get; set; }
}