using Medical_Inventory.Models;

namespace Medical_Inventory.Data.IRepository.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Update(Product obj)
    {
        var product = _dbContext.Products!.FirstOrDefault(c => c.Id == obj.Id);

        if (product is null) return;

        product.Name = obj.Name;
        product.Strength = obj.Strength;
        product.Generic = obj.Generic;
        product.Details = obj.Details;
        product.Category = obj.Category;
    }
}