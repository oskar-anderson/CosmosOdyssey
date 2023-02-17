using System.ComponentModel;

namespace WebDTO;

public class CreateOrder
{
    [DisplayName("First name")]
    public required string FirstName { get; set; } = default!;
    [DisplayName("Last name")]
    public required string LastName { get; set; } = default!;
    public required DAL.App.DTO.ProvidedRoute ProvidedRoute { get; set; }
    public required Guid ProvidedRouteId { get; set; }
}