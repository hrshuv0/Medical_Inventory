using Medical_Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_Inventory.Data.IRepository.Repository;

public class GenericRepository : IGenericRepository
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Generic?>? GetFirstOrDefault(int? id)
    {
        if (id is null) return null;

        var result = _dbContext.Generic!.FirstOrDefaultAsync(c => c.Id == id);

        return result;
    }

    public async Task<IEnumerable<Generic>?> GetAll()
    {
        var result = _dbContext.Generic!;

        var data = await result.ToListAsync();

        return data;
    }

    public async Task Add(Generic entity)
    {
        await _dbContext.Generic!.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Generic entity)
    {
        try
        {
            _dbContext.Generic!.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Remove(Generic entity)
    {
        var products = from p in _dbContext.Products!
                .Include(p => p.Generic)
                .DefaultIfEmpty()
                .Where(p => p.Generic!.Id == entity.Id)
            select p;

        if (products is not null)
        {
            foreach (var product in products)
            {
                if (product.GenericId == entity.Id)
                {
                    product.Generic = null;
                    product.GenericId = null;
                }
            }

            await _dbContext.SaveChangesAsync();
        }
        
        _dbContext.Generic!.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}

