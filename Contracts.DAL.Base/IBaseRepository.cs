namespace Contracts.DAL.Base;

public interface IBaseRepository<TDalEntity>
    where TDalEntity : class
{
    Task<List<TDalEntity>> GetAllAsyncBase();
    Task<TDalEntity?> FirstOrDefault(Guid id);
    Task<TDalEntity> Add(TDalEntity entity);
    Task<TDalEntity> UpdateAsync(TDalEntity entity);
    Task<TDalEntity> RemoveAsync(TDalEntity entity);
    Task<TDalEntity> RemoveAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}