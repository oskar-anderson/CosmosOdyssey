using Contracts.DAL.App.Repository;

namespace Contracts.DAL.App;

public interface IAppUnitOfWork
{
    ICompanyRepository Companies { get; }
    ILocationRepository Locations { get; }
    IOrderRepository Orders { get; }
    IOrderLineRepository OrderLines { get; }
    IPriceListRepository PriceLists { get; }
    IProvidedRouteRepository ProvidedRoutes { get; }
}