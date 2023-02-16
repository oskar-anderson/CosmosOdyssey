using Contracts.DAL.App.Repository;
using DAL.App.DTO;
using Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OrderLine = Domain.App.OrderLine;

namespace DAL.App.EF.Repositories;

public class OrderRepository : IOrderRepository
{
    private OrderMapper Mapper = new();
    private DbContext RepoDbContext;
    private DbSet<Domain.App.Order> RepoDbSet;

    public OrderRepository(AppDbContext dbContext)
    {
        RepoDbContext = dbContext;
        RepoDbSet = RepoDbContext.Set<Domain.App.Order>();
        if (RepoDbSet == null)
        {
            throw new ArgumentNullException($"{nameof(Domain.App.Company)} was not found as DbSet!");
        }
    }

    // Convenience method to include everything for mapping
    public IIncludableQueryable<Domain.App.Order, ICollection<OrderLine>?> GetIncludes(DbSet<Domain.App.Order> dbSet)
    {
        return dbSet
            .Include(x => x.OrderLines);
    }

    public async Task<List<Order>> GetAllAsyncBase()
    {
        return await GetIncludes(RepoDbSet).Select(x => Mapper.DomainToDal(x)).ToListAsync();
    }

    public async Task<Order?> FirstOrDefault(Guid id)
    {
        return Mapper.DomainToDal(await GetIncludes(RepoDbSet).FirstAsync(x => x.Id.Equals(id)));
    }

    public async Task<Order> Add(Order entity)
    {
        var domainEntity = Mapper.DalToDomain(entity);
        var result = (await RepoDbSet.AddAsync(domainEntity)).Entity;
        return Mapper.DomainToDal(result);
    }

    public async Task<Order> UpdateAsync(Order entity)
    {
        var domainEntity = Mapper.DalToDomain(entity);
        var updatedDomainEntity = RepoDbSet.Update(domainEntity).Entity;
        return Mapper.DomainToDal(updatedDomainEntity);
    }

    public async Task<Order> RemoveAsync(Order entity)
    {
        var domainEntity = Mapper.DalToDomain(entity);
        var removedEntity = RepoDbSet.Remove(domainEntity).Entity;
        return Mapper.DomainToDal(removedEntity);
    }

    public async Task<Order> RemoveAsync(Guid id)
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
}