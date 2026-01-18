using System.Linq.Expressions;
public interface IGenericRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(ICollection<T> entities);
    Task<long> CountAsync(Expression<Func<T, bool>>? predicate = null);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<ICollection<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Ulid id);
    void Remove(T entity);
    void RemoveRange(ICollection<T> entities);
    void Update(T entity);
}
