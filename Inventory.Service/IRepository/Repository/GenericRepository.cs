using Entities;
using Inventory.DAL.DbContext;
using Inventory.Utility.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Service.IRepository.Repository;

public class GenericRepository : IGenericRepository
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Generic?>? GetFirstOrDefault(long? id)
    {
        if (id is null) return null;

        var result = await _dbContext.Generic!
            .Include(c => c.CreatedBy)
            .Include(c => c.UpdatedBy)
            .FirstOrDefaultAsync(c => c.Id == id);

        if(result == null) throw new NotFoundException();

        return result;
    }

    public async Task<IEnumerable<Generic>?> GetAll()
    {
        var result = _dbContext.Generic!
            .Include(c => c.CreatedBy)
            .Include(c => c.UpdatedBy)
            .ToListAsync();

        var data = await result;

        return data;
    }

    public async Task Add(Generic entity)
    {
        await _dbContext.Generic!.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public void Update(Generic entity)
    {
        try
        {
            var category = _dbContext.Generic!.FirstOrDefault(c => c.Id == entity.Id);

            if (category is null)
                throw new NotFoundException("");

            category.Name = entity.Name;
            category.UpdatedTime = DateTime.Now;
            category.UpdatedById = entity.UpdatedById;
        }
        catch (Exception ex)
        {
            //
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

    public async Task<Generic?> GetByName(string name)
    {
        try
        {
            var result = await _dbContext.Generic!.FirstOrDefaultAsync(p => p.Name == name);

            if (result is not null)
                throw new DuplicationException(name!);

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Generic?> GetByName(string name, long id)
    {
        try
        {
            var result = await _dbContext.Generic!.FirstOrDefaultAsync(p => p.Name == name);

            if (result is not null)
            {
                if (result.Id != id)
                    throw new DuplicationException(name!);
            }

            return result;
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}

