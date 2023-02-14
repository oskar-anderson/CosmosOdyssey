using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO;

public class Location
{
    public required Guid Id { get; set; }
    
    [MaxLength(255)]
    [Display(Name = "Planetary system name")]
    public required string PlanetarySystemName { get; set; } = default!;
    
    [MaxLength(255)]
    [Display(Name = "Planet name")]
    public required string PlanetName { get; set; } = default!;
    
    [MaxLength(255)]
    [Display(Name = "Port name")]
    public required string PlanetLocationName { get; set; } = default!;
    
    [StringLength(3, MinimumLength = 3)]
    [Display(Name = "IATA")]
    public required string UniquePlanetLocation3LetterIdentifier { get; set; } = default!;
}