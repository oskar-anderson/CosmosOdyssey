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
    
    // same type FromRoutes and DestinationRoutes take part in multiple relationships so it has to be annotated.
    // Otherwise "InvalidOperationException: Unable to determine the relationship represented by navigation 'Location.DestinationRoutes' of type 'ICollection<ProvidedRoute>'." is thrown
    [InverseProperty(nameof(Domain.App.ProvidedRoute.FromLocation))]
    public virtual ICollection<ProvidedRoute>? FromRoutes { get; set; }
    
    [InverseProperty(nameof(Domain.App.ProvidedRoute.DestinationLocation))]
    public virtual ICollection<ProvidedRoute>? DestinationRoutes { get; set; }
}