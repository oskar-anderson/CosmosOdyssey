namespace DAL.App.DTO;

public class Company
{
    public required Guid Id { get; set; }
    public required string Name { get; set; } = default!;
}