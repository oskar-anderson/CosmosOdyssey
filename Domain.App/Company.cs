using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class Company : DomainEntityMetadata
{
    [MaxLength(255)]
    public string Name { get; set; } = default!;

    public ICollection<ProvidedRoute>? ProvidedRoutes;

    /*
     *
     * Spacegenix
     * Spacelux
     * Space Odyssey
     * Explore Origin
     * SpaceX
     * Space Piper
     * Galaxy Express
     * Travel Nova
     * Explore Dynamite
     * Space Voyager
     */
}