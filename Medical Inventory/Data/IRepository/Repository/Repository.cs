using System.Linq.Expressions;
using Medical_Inventory.Exceptions;
using Medical_Inventory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Medical_Inventory.Data.IRepository.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        // _dbContext.Products!.Include(p => p.Category);
        
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<T?>? GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties=null)
    {
        try
        {
            IQueryable<T> result = _dbSet.Where(filter);

            if (includeProperties is not null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    result = result.Include(property).DefaultIfEmpty();
            }

            var data = await result.FirstOrDefaultAsync();

            if (data is null)
                throw new NotFoundException("");

            return data;

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<T>?>? GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties=null)
    {
        IQueryable<T> result = _dbSet;
        
        if(filter is not null)
            result = result.Where(filter);

        var d = result.ToList();

        if (includeProperties is not null && d is not null && d.Any())
        {
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                result = result.Include(property).DefaultIfEmpty();
        }

        var data = await result!.ToListAsync();

        return data;
    }

    

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public IdentityUser? GetFirstOrDefaultUser(string id)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

        return user;
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}