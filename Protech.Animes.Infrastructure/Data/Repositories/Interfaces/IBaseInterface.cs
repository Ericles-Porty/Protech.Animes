namespace Protech.Animes.Infrastructure.Data.Repositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T?> GetByIdAsync(int id);
    public Task<T> CreateAsync(T entity);
    public Task<T?> UpdateAsync(int id, T entity);
    public Task<bool> DeleteAsync(int id);
}