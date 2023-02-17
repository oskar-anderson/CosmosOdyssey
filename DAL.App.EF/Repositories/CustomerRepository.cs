using Contracts.DAL.App.Repository;
using DAL.App.DTO;
using Domain.App;
using Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Customer = Domain.App.Customer;
using Order = Domain.App.Order;

namespace DAL.App.EF.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private CustomerMapper Mapper = new();
    private DbContext RepoDbContext;
    private DbSet<Domain.App.Customer> RepoDbSet;

    public CustomerRepository(AppDbContext dbContext)
    {
        RepoDbContext = dbContext;
        RepoDbSet = RepoDbContext.Set<Domain.App.Customer>();
        if (RepoDbSet == null)
        {
            throw new ArgumentNullException($"{nameof(Domain.App.Company)} was not found as DbSet!");
        }
    }
    
    // Convenience method to include everything for mapping
    public IIncludableQueryable<Customer, ICollection<Order>?> GetIncludes(DbSet<Domain.App.Customer> dbSet)
    {
        return dbSet
            .Include(x => x.Orders);
    }


    public async Task<List<DAL.App.DTO.Customer>> GetAllAsyncBase()
    {
        return await GetIncludes(RepoDbSet).Select(x => Mapper.DomainToDal(x)).ToListAsync();
    }

    public async Task<DAL.App.DTO.Customer?> FirstOrDefault(Guid id)
    {
        return Mapper.DomainToDal(await GetIncludes(RepoDbSet).FirstAsync(x => x.Id.Equals(id)));
    }

    public async Task<DAL.App.DTO.Customer> Add(DAL.App.DTO.Customer entity)
    {
        var domainEntity = Mapper.DalToDomain(entity);
        var result = (await RepoDbSet.AddAsync(domainEntity)).Entity;
        return Mapper.DomainToDal(result);
    }

    public async Task<DAL.App.DTO.Customer> UpdateAsync(DAL.App.DTO.Customer entity)
    {
        var domainEntity = Mapper.DalToDomain(entity);
        var updatedDomainEntity = RepoDbSet.Update(domainEntity).Entity;
        return Mapper.DomainToDal(updatedDomainEntity);
    }

    public async Task<DAL.App.DTO.Customer> RemoveAsync(DAL.App.DTO.Customer entity)
    {
        var domainEntity = Mapper.DalToDomain(entity);
        var removedEntity = RepoDbSet.Remove(domainEntity).Entity;
        return Mapper.DomainToDal(removedEntity);
    }

    public async Task<DAL.App.DTO.Customer> RemoveAsync(Guid id)
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

    public async Task<DAL.App.DTO.Customer?> GetCustomer_FirstOrDefaultAsync_WhereCustomerFirstNameEqualsArg1AndLastNameEqualsArg2(string firstName, string lastName)
    {
        var customer = await GetIncludes(RepoDbSet).FirstOrDefaultAsync(x => x.FirstName == firstName && x.LastName == lastName);
        if (customer == null) return null;
        return Mapper.DomainToDal(customer);
    }
}