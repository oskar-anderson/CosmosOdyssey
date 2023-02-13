namespace Mapper;

public class PriceListMapper
{
    public Domain.App.PriceList DalToDomain(DAL.App.DTO.PriceList x)
    {
        return new Domain.App.PriceList()
        {
            Id = x.Id,
            Counter = x.Counter,
            ValidUntil = x.ValidUntil,
            ValueJson = x.ValueJson
        };
    }

    public DAL.App.DTO.PriceList DomainToDal(Domain.App.PriceList x)
    {
        return new DAL.App.DTO.PriceList()
        {
            Id = x.Id,
            Counter = x.Counter,
            ValidUntil = x.ValidUntil,
            ValueJson = x.ValueJson,
        };
    }
}