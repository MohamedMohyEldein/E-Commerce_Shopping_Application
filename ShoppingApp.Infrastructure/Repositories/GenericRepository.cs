using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Infrastructure.Persistence;

namespace ShoppingApp.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Read Operations
        public async Task<T?> GetByIdAsync(Ulid id)
        {
            // Implementation to retrieve an entity by its Ulid
            return await _dbSet.FindAsync(id);
        }
        public async Task<ICollection<T>> GetAllAsync()
        {
            // Implementation to retrieve all entities of type T
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            // Implementation to retrieve entities based on a predicate
            return await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
        }
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            // Implementation to check if any entity matches the predicate
            return await _dbSet.AnyAsync(predicate);
        }
        public async Task<long> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            // Implementation to count all entities of type T
            return predicate is null ? await _dbSet.LongCountAsync() : await _dbSet.Where(predicate).LongCountAsync();
        }

        // Write Operations
        public async Task<T> AddAsync(T entity)
        {
            // Implementation to add a new entity
            await _dbSet.AddAsync(entity);
            return entity;
        }
        public async Task AddRangeAsync(ICollection<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
        public void Update(T entity)
        {
            // Implementation to update an existing entity
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
        public void Remove(T entity)
        {
            // Implementation to remove an entity
            _dbSet.Remove(entity);
        }
        public void RemoveRange(ICollection<T> entities)
        {
            // Implementation to remove multiple entities
            _dbSet.RemoveRange(entities);
        }
    }
}
