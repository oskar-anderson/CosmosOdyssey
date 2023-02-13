namespace Mapper;

public class LocationMapper
{
    public Domain.App.Location DalToDomain(DAL.App.DTO.Location x)
    {
        return new Domain.App.Location()
        {
            Id = x.Id,
            SolarSystemName = x.SolarSystemName,
            PlanetName = x.PlanetName,
            PlanetLocationName = x.PlanetLocationName,
            UniquePlanetLocation3LetterIdentifier = x.UniquePlanetLocation3LetterIdentifier,
        };
    }

    public DAL.App.DTO.Location DomainToDal(Domain.App.Location x)
    {
        return new DAL.App.DTO.Location()
        {
            Id = x.Id,
            SolarSystemName = x.SolarSystemName,
            PlanetName = x.PlanetName,
            PlanetLocationName = x.PlanetLocationName,
            UniquePlanetLocation3LetterIdentifier = x.UniquePlanetLocation3LetterIdentifier,
        };
    }
}