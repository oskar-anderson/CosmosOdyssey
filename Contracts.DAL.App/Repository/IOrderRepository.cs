using Contracts.DAL.Base;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repository;

public interface IOrderRepository : IBaseRepository<Order>
{
    public Task<IEnumerable<OrderWithCustomer>> GetOrdersWithCustomer_WhereCustomerIdEqualsArg(Guid customerId);
}