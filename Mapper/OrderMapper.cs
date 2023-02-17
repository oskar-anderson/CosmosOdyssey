namespace Mapper;

public class OrderMapper
{
    public Domain.App.Order DalToDomain(DAL.App.DTO.Order x)
    {
        return new Domain.App.Order()
        {
            Id = x.Id,
            RouteName = x.RouteName,
            Price = x.Price,
            FlightStart = x.FlightStart,
            FlightEnd = x.FlightEnd,
            CompanyName = x.CompanyName,
            DateOfPurchase = x.DateOfPurchase,
            CustomerId = x.CustomerId
        };
    }

    public DAL.App.DTO.Order DomainToDal(Domain.App.Order x)
    {
        return new DAL.App.DTO.Order()
        {
            Id = x.Id,
            RouteName = x.RouteName,
            Price = x.Price,
            FlightStart = x.FlightStart,
            FlightEnd = x.FlightEnd,
            CompanyName = x.CompanyName,
            DateOfPurchase = x.DateOfPurchase,
            CustomerId = x.CustomerId
        };
    }
    
    public DAL.App.DTO.OrderWithCustomer DomainToDalWithCustomer(Domain.App.Order x)
    {
        return new DAL.App.DTO.OrderWithCustomer
        {
            Id = x.Id,
            RouteName = x.RouteName,
            Price = x.Price,
            FlightStart = x.FlightStart,
            FlightEnd = x.FlightEnd,
            CompanyName = x.CompanyName,
            DateOfPurchase = x.DateOfPurchase,
            CustomerId = x.CustomerId,
            CustomerFirstName = x.Customer.FirstName,
            CustomerLastName = x.Customer.LastName
        };
    }
}