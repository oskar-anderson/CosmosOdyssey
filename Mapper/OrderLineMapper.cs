namespace Mapper;

public class OrderLineMapper
{
    public Domain.App.OrderLine DalToDomain(DAL.App.DTO.OrderLine x)
    {
        return new Domain.App.OrderLine()
        {
            Id = x.Id,
            OrderId = x.OrderId,
            RouteName = x.RouteName,
            Price = x.Price,
            FlightStart = x.FlightStart,
            FlightEnd = x.FlightEnd,
            CompanyName = x.CompanyName,
        };
    }

    public DAL.App.DTO.OrderLine DomainToDal(Domain.App.OrderLine x)
    {
        return new DAL.App.DTO.OrderLine()
        {
            Id = x.Id,
            OrderId = x.OrderId,
            RouteName = x.RouteName,
            Price = x.Price,
            FlightStart = x.FlightStart,
            FlightEnd = x.FlightEnd,
            CompanyName = x.CompanyName,
        };
    }
}