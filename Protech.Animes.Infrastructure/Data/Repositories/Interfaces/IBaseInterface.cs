namespace Protech.Animes.Infrastructure.Data.Repositories.Interfaces;

public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : class where TPrimaryKey : struct
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsyncWithouTracking();
    Task<TEntity?> GetByIdAsync(TPrimaryKey id);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(TPrimaryKey id);
}