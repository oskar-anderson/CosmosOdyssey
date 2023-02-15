using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App;

public class Location : DomainEntityMetadata
{
    [MaxLength(255)] 
    public string PlanetarySystemName { get; set; } = default!;
    
    [MaxLength(255)] 
    public string PlanetName { get; set; } = default!;

    [MaxLength(255)]
    public string PlanetLocationName { get; set; } = default!;
    
    [StringLength(3, MinimumLength = 3)]
    public string UniquePlanetLocation3LetterIdentifier { get; set; } = default!;

    public ICollection<ProvidedRoute>? FromRoutes;
    public ICollection<ProvidedRoute>? DestinationRoutes;
}