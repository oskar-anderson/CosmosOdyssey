namespace DAL.App.DTO;

public class PriceList
{
    public required Guid Id { get; set; }
    
    public required int Counter { get; set; }
    public required DateTime ValidUntil { get; set; }

    public required string ValueJson { get; set; } = default!;
}