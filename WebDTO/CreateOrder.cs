namespace WebDTO;

public class CreateOrder
{
    public required string FirstName { get; set; } = default!;
    public required string LastName { get; set; } = default!;
    public required DAL.App.DTO.ProvidedRoute ProvidedRoute { get; set; }
}