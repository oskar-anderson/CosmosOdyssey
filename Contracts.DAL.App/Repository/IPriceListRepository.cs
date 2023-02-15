using Contracts.DAL.Base;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repository;

public interface IPriceListRepository : IBaseRepository<PriceList>
{
    public Task<int> OrderByValidUntilDescendingThenSkipNThenDeleteAll(int lastNElementsToDelete);
}