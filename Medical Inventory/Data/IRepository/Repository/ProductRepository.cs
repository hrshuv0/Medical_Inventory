using Medical_Inventory.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

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
        var productlist = _dbContext.Products!.DefaultIfEmpty().ToList();


        var product = _dbContext.Products!.DefaultIfEmpty().FirstOrDefault(c => c.Id == obj.Id)!;
        //var product = _dbContext.Products!.Where(p => p.Id == obj.Id).FirstOrDefault();

        if (product is null) return;

        product.Name = obj.Name;
        product.Strength = obj.Strength;
        product.Generic = obj.Generic;
        product.Details = obj.Details;
        product.CategoryId = obj.CategoryId;
    }
}