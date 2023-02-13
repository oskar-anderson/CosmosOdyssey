namespace DAL.App.DTO;

public class Order
{
    public required Guid Id { get; set; }
    
    public required string FirstName { get; set; } = default!;
    
    public required string LastName { get; set; } = default!;

    public required DateTime DateOfPurchase { get; set; } = default;

    public required ICollection<OrderLine> OrderLines { get; set; } = default!;
}