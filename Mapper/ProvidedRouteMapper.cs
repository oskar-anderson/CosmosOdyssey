namespace Mapper;

public class ProvidedRouteMapper
{
    public Domain.App.ProvidedRoute DalToDomain(DAL.App.DTO.ProvidedRoute x)
    {
        return new Domain.App.ProvidedRoute()
        {
            Id = x.Id,
            FromLocationId = x.FromLocationId,
            FromLocation = new LocationMapper().DalToDomain(x.FromLocation),
            DestinationLocationId = x.DestinationLocationId,
            DestinationLocation = new LocationMapper().DalToDomain(x.DestinationLocation),
            CompanyId = x.CompanyId,
            Company = new CompanyMapper().DalToDomain(x.Company),
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
            FromLocation = new LocationMapper().DomainToDal(x.FromLocation),
            DestinationLocationId = x.DestinationLocationId,
            DestinationLocation = new LocationMapper().DomainToDal(x.DestinationLocation),
            CompanyId = x.CompanyId,
            Company = new CompanyMapper().DomainToDal(x.Company),
            Price = x.Price,
            FlightStart = x.FlightStart,
            FlightEnd = x.FlightEnd,
        };
    }
}