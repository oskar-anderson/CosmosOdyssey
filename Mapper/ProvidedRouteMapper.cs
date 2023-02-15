namespace Mapper;

public class ProvidedRouteMapper
{
    public Domain.App.ProvidedRoute DalToDomain(DAL.App.DTO.ProvidedRoute x)
    {
        return new Domain.App.ProvidedRoute()
        {
            Id = x.Id,
            FromLocationId = x.FromLocationId,
            FromLocation = x.FromLocation == null ? null : new LocationMapper().DalToDomain(x.FromLocation),
            DestinationLocationId = x.DestinationLocationId,
            DestinationLocation = x.DestinationLocation == null ? null : new LocationMapper().DalToDomain(x.DestinationLocation),
            Distance = x.Distance,
            CompanyId = x.CompanyId,
            Company = x.Company == null ? null : new CompanyMapper().DalToDomain(x.Company),
            Price = x.Price,
            FlightStart = x.FlightStart,
            FlightEnd = x.FlightEnd,
        };
    }

    public DAL.App.DTO.ProvidedRoute DomainToDal(Domain.App.ProvidedRoute x)
    {
        return new DAL.App.DTO.ProvidedRoute()
        {
            Id = x.Id,
            FromLocationId = x.FromLocationId,
            FromLocation = null,
            DestinationLocationId = x.DestinationLocationId,
            DestinationLocation = null,
            Distance = x.Distance,
            CompanyId = x.CompanyId,
            Company = null,
            Price = x.Price,
            FlightStart = x.FlightStart,
            FlightEnd = x.FlightEnd,
        };
    }
}