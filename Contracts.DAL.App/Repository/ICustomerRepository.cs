using Contracts.DAL.Base;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repository;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    public Task<Customer?> GetCustomer_FirstOrDefaultAsync_WhereCustomerFirstNameEqualsArg1AndLastNameEqualsArg2(string firstName, string lastName);
}