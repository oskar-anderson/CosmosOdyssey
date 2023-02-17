namespace Mapper;

public class ProvidedRouteMapper
{
    public Domain.App.ProvidedRoute DalToDomain(DAL.App.DTO.ProvidedRoute x)
    {
        var fromLocation = new LocationMapper().DalToDomain(x.FromLocation);
        var destinationLocation = new LocationMapper().DalToDomain(x.DestinationLocation);
        var company = new CompanyMapper().DalToDomain(x.Company);
        return new Domain.App.ProvidedRoute()
        {
            Id = x.Id,
            PriceListId = x.PriceList.Id,
            FromLocationId = fromLocation.Id,
            FromLocation = fromLocation,
            DestinationLocationId = destinationLocation.Id,
            DestinationLocation = destinationLocation,
            Distance = x.Distance,
            CompanyId = company.Id,
            Company = company,
            Price = x.Price,
            FlightStart = x.FlightStart,
            FlightEnd = x.FlightEnd,
        };
    }
    
    public Domain.App.ProvidedRoute NavigationlessToDomain(DAL.App.DTO.ProvidedRouteNavigationless x)
    {
        return new Domain.App.ProvidedRoute()
        {
            Id = x.Id,
            PriceListId = x.PriceListId,
            FromLocationId = x.FromLocationId,
            DestinationLocationId = x.DestinationLocationId,
            Distance = x.Distance,
            CompanyId = x.CompanyId,
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
            PriceList = new PriceListMapper().DomainToDal(x.PriceList),
            FromLocation = new LocationMapper().DomainToDal(x.FromLocation),
            DestinationLocation = new LocationMapper().DomainToDal(x.DestinationLocation),
            Distance = x.Distance,
            Company = new CompanyMapper().DomainToDal(x.Company),
            Price = x.Price,
            FlightStart = x.FlightStart,
            FlightEnd = x.FlightEnd,
        };
    }
}