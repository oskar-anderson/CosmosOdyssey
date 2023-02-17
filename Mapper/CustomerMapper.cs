namespace Mapper;

public class CustomerMapper
{
    public Domain.App.Customer DalToDomain(DAL.App.DTO.Customer x)
    {
        return new Domain.App.Customer()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Orders = x.Orders.Select(c => new OrderMapper().DalToDomain(c)).ToList(),
        };
    }

    public DAL.App.DTO.Customer DomainToDal(Domain.App.Customer x)
    {
        return new DAL.App.DTO.Customer()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Orders = x.Orders.Select(c => new OrderMapper().DomainToDal(c)).ToList(),
        };
    }
}