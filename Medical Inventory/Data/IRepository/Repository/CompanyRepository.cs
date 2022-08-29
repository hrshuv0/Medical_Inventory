using Medical_Inventory.Exceptions;
using Medical_Inventory.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
        try
        {
            var existsCompany = _dbContext.Company!.FirstOrDefaultAsync(c => c.Id == entity.Id);

            if (existsCompany != null)
            {
                throw new DuplicationException(entity.Name);
            }

            await _dbContext.Company!.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {

            throw;
        }
        
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
                .Include(p => p.Company)
                .DefaultIfEmpty()
                .Where(p => p.Company!.Id == entity.Id)
            select p;

        if (products is not null)
        {
            foreach (var product in products)
            {
                if (product.CompanyId != entity.Id) continue;
                
                product.Company = null;
                product.CompanyId = null;
            }

            await _dbContext.SaveChangesAsync();
        }
        
        _dbContext.Company!.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}

