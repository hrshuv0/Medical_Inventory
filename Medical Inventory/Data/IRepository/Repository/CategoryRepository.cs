using Medical_Inventory.Models;

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
}