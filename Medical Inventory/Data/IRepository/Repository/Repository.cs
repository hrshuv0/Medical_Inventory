using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Medical_Inventory.Data.IRepository.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public async Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> filter)
    {
        var result = await _dbSet.Where(filter).FirstOrDefaultAsync();

        return result;
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> result = _dbSet;
        
        if(filter is not null)
            result = result.Where(filter);
        
        return await result.ToListAsync();
    }

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}