using Contracts.DAL.App;
using Contracts.DAL.App.Repository;
using DAL.App.EF.Repositories;

namespace DAL.App.EF;

public class AppUnitOfWork : IAppUnitOfWork
{
    protected readonly AppDbContext UowDbContext;

    public AppUnitOfWork(AppDbContext uowContext)
    {
        UowDbContext = uowContext;
    }

    public ICompanyRepository Companies => new CompanyRepository(UowDbContext);
    public ILocationRepository Locations => new LocationRepository(UowDbContext);
    public ICustomerRepository Customers => new CustomerRepository(UowDbContext);
    public IOrderRepository Orders => new OrderRepository(UowDbContext);
    public IPriceListRepository PriceLists => new PriceListRepository(UowDbContext);
    public IProvidedRouteRepository ProvidedRoutes => new ProvidedRouteRepository(UowDbContext);

    public async Task<int> SaveChangesAsync()
    {
        return await UowDbContext.SaveChangesAsync();
    }
    
    public int SaveChanges()
    {
        return UowDbContext.SaveChanges();
    }
}