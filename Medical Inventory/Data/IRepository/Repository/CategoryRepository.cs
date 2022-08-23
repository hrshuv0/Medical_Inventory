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
    
    public void Update(Category obj)
    {
        var category = _dbContext.Categories!.FirstOrDefault(c => c.Id == obj.Id);

        if (category is null) return;

        category.Name = obj.Name;
    }


    
    public void Remove(int id)
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
}