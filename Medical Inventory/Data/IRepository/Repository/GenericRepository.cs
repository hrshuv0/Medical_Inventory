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
        var result = await _dbContext.Generic!.ToListAsync();

        return result;
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
        _dbContext.Generic!.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}

