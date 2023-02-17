namespace IthsBoardGamers.DataAccess.Services;

public interface IRepository<T>
{
    Task AddAsync(T item);
    Task AddManyAsync(IEnumerable<T> items);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(object id);
    Task<T> UpdateAsync(T item, object id);
    Task DeleteAsync(object id);
    Task DeleteManyAsync(IEnumerable<T> items);
}