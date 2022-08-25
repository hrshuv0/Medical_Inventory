using Medical_Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_Inventory.Data.IRepository.Repository;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CompanyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Company?>? GetFirstOrDefault(int? id)
    {
        if (id is null) return null;

        var result = _dbContext.Company!.FirstOrDefaultAsync(c => c.Id == id);

        return result;
    }

    public async Task<IEnumerable<Company>?> GetAll()
    {
        var result = _dbContext.Company!;

        var data = await result.ToListAsync();

        return data;
    }

    public async Task Add(Company entity)
    {
        await _dbContext.Company!.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Company entity)
    {
        try
        {
            _dbContext.Company!.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Remove(Company entity)
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

            // await _dbContext.SaveChangesAsync();
        }
        
        _dbContext.Company!.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}

