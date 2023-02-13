namespace Mapper;

public class CompanyMapper
{
    public Domain.App.Company DalToDomain(DAL.App.DTO.Company x)
    {
        return new Domain.App.Company()
        {
            Id = x.Id,
            Name = x.Name
        };
    }

    public DAL.App.DTO.Company DomainToDal(Domain.App.Company x)
    {
        return new DAL.App.DTO.Company()
        {
            Id = x.Id,
            Name = x.Name
        };
    }
}