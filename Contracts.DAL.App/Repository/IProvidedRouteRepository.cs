using Contracts.DAL.Base;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repository;

public interface IProvidedRouteRepository : IBaseRepository<ProvidedRoute>
{
    public Task<List<Location>> ProvidedRoutes_IncludeLocation_WhereFromLocationIdEqualsArg_SelectDestinationLocation_Distinct_ToListAsync(Guid fromLocationId);
    public Task<List<ProvidedRoute>> ProvidedRoutes_GetAll_WhereFromLocationIdEqualsArg1AndToLocationIdEqualsArg2_ToListAsync(Guid fromLocationId, Guid toLocationId);
    public void RemoveAll();
}