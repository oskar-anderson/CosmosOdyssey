namespace Mapper;

public class OrderMapper
{
    public Domain.App.Order DalToDomain(DAL.App.DTO.Order x)
    {
        return new Domain.App.Order()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            DateOfPurchase = x.DateOfPurchase,
            OrderLines = x.OrderLines.Select(c => new OrderLineMapper().DalToDomain(c)).ToList(),
        };
    }

    public DAL.App.DTO.Order DomainToDal(Domain.App.Order x)
    {
        return new DAL.App.DTO.Order()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            DateOfPurchase = x.DateOfPurchase,
            OrderLines = x.OrderLines.Select(c => new OrderLineMapper().DomainToDal(c)).ToList(),
        };
    }
}