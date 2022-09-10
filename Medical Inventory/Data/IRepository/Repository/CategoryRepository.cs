using Medical_Inventory.Exceptions;
using Medical_Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_Inventory.Data.IRepository.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async void Update(Category obj)
    {
        try
        {
            var category = _dbContext.Categories!.FirstOrDefault(c => c.Id == obj.Id);

            if (category is null)
                throw new NotFoundException("");

            category.Name = obj.Name;
        }
        catch (Exception)
        {

            throw;
        }
    }


    
    public void Remove(long id)
    {
        var category = _dbContext.Categories!.FirstOrDefault(c => c.Id == id)!;

        if (category is null) return;

        var products = from p in _dbContext.Products!
                       .Include(p => p.Category)
                       .DefaultIfEmpty()
                       .Where(p => p.Category!.Id == id)
                       select p;
        
        
        if (products is not null)
        {
            foreach (var product in products)
            {
                if(product.CategoryId == id)
                {
                    product.Category = null;
                    product.CategoryId = null;
                }
                
            }
            _dbContext.SaveChanges();
        }
        
        
        Remove(category);
    }

    public async Task<Category?> GetByName(string? name)
    {
        try
        {
            var result = await _dbContext.Categories!.FirstOrDefaultAsync(p => p.Name == name);

            if(result is not null)
                throw new DuplicationException(name!);

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
}