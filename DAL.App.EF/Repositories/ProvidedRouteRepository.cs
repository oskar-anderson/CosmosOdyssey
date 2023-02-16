using Contracts.DAL.App.Repository;
using DAL.App.DTO;
using Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DAL.App.EF.Repositories;

public class ProvidedRouteRepository : IProvidedRouteRepository
{
    private ProvidedRouteMapper Mapper = new();
    private DbContext RepoDbContext;
    private DbSet<Domain.App.ProvidedRoute> RepoDbSet;

    public ProvidedRouteRepository(AppDbContext dbContext)
    {
        RepoDbContext = dbContext;
        RepoDbSet = RepoDbContext.Set<Domain.App.ProvidedRoute>();
        if (RepoDbSet == null)
        {
            throw new ArgumentNullException($"{nameof(Domain.App.Company)} was not found as DbSet!");
        }
    }

    // Convenience method to include everything for mapping
    public IIncludableQueryable<Domain.App.ProvidedRoute, Domain.App.Location?> GetIncludes(DbSet<Domain.App.ProvidedRoute> dbSet)
    {
        return dbSet
            .Include(x => x.Company)
            .Include(x => x.FromLocation)
            .Include(x => x.DestinationLocation);
    }


    public async Task<List<ProvidedRoute>> GetAllAsyncBase()
    {
        return await GetIncludes(RepoDbSet).Select(x => Mapper.DomainToDal(x)).ToListAsync();
    }

    public async Task<ProvidedRoute?> FirstOrDefault(Guid id)
    {
        return Mapper.DomainToDal(await GetIncludes(RepoDbSet).FirstAsync(x => x.Id.Equals(id)));
    }

    public async Task<ProvidedRoute> Add(ProvidedRoute entity)
    {
        var domainEntity = Mapper.DalToDomain(entity);
        var result = (await RepoDbSet.AddAsync(domainEntity)).Entity;
        return Mapper.DomainToDal(result);
    }

    public async Task<ProvidedRoute> UpdateAsync(ProvidedRoute entity)
    {
        var domainEntity = Mapper.DalToDomain(entity);
        var updatedDomainEntity = RepoDbSet.Update(domainEntity).Entity;
        return Mapper.DomainToDal(updatedDomainEntity);
    }

    public async Task<ProvidedRoute> RemoveAsync(ProvidedRoute entity)
    {
        var domainEntity = Mapper.DalToDomain(entity);
        var removedEntity = RepoDbSet.Remove(domainEntity).Entity;
        return Mapper.DomainToDal(removedEntity);
    }

    public async Task<ProvidedRoute> RemoveAsync(Guid id)
    {
        var domainEntity = await RepoDbSet.FirstOrDefaultAsync(e => e.Id.Equals(id));
        if (domainEntity == null)
        {
            throw new ArgumentException("Entity to be updated was not found in data source!");
        }

        return Mapper.DomainToDal(RepoDbSet.Remove(domainEntity).Entity);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await RepoDbSet.AnyAsync(x => x.Id.Equals(id));
    }
    
    public async Task Add(ProvidedRouteNavigationless entity)
    {
        var domainEntity = Mapper.NavigationlessToDomain(entity);
        await RepoDbSet.AddAsync(domainEntity);
    }
    
    public async Task<List<Location>> ProvidedRoutes_IncludeLocation_WhereFromLocationIdEqualsArg_SelectDestinationLocation_Distinct_ToListAsync(Guid fromLocationId)
    {
        var locationMapper = new LocationMapper();
        return await GetIncludes(RepoDbSet)
            .Where(x => x.FromLocationId == fromLocationId)
            .Select(x => locationMapper.DomainToDal(x.DestinationLocation))
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<ProvidedRoute>> ProvidedRoutes_GetAll_WhereFromLocationIdEqualsArg1AndToLocationIdEqualsArg2_ToListAsync(Guid fromLocationId,
        Guid toLocationId)
    {
        return await GetIncludes(RepoDbSet)
            .Where(x => x.FromLocationId == fromLocationId && x.DestinationLocationId == toLocationId)
            .Select(x => Mapper.DomainToDal(x))
            .ToListAsync();
    }
}