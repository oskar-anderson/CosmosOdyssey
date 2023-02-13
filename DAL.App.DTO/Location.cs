namespace DAL.App.DTO;

public class Location
{
    public required Guid Id { get; set; }
    
    public required string SolarSystemName { get; set; } = default!;
    
    public required string PlanetName { get; set; } = default!;
    
    public required string PlanetLocationName { get; set; } = default!;
    
    public required string UniquePlanetLocation3LetterIdentifier { get; set; } = default!;
}