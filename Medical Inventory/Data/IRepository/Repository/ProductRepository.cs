using Medical_Inventory.Exceptions;
using Medical_Inventory.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Medical_Inventory.Data.IRepository.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
        

    public async Task<Product?> GetByName(string? name)
    {
        try
        {
            var result = await _dbContext.Products!.FirstOrDefaultAsync(p => p.Name == name);

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    async Task<Product?> IProductRepository.GetByName(string name, long id)
    {
        try
        {
            var product = await _dbContext.Products!.Where(p => p.Name == name).FirstOrDefaultAsync();

            if (product is not null)
            {
                if (product!.Id != id)
                    throw new DuplicationException(name);
            }
            return product;

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Product?>? GetFirstOrDefault(long id)
    {
        try
        {
            var product = await _dbContext.Products!
                .Include(p => p.Category)
                .Include(p => p.Generic)
                .Include(p => p.Company)
                .Include(p =>p.UpdatedBy)
                .Include(p => p.CreatedBy)
                .Include(p => p.RecommandedPatients)!
                .ThenInclude(r => r.PatientGroup)
                .AsNoTracking()
                .Where(p => p.Id == id)
                .DefaultIfEmpty()
                .FirstOrDefaultAsync();

            return product;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<Product>?>? GetAll(long? categoryId = null)
    {
        try
        {
            if(categoryId == null)
            {
                var productList = await _dbContext.Products!
                .Include(p => p.Category)
                .Include(p => p.Generic)
                .Include(p => p.Company)
                .Include(p => p.RecommandedPatients)!
                .ThenInclude(r => r.PatientGroup)
                .DefaultIfEmpty()
                .ToListAsync();

                return productList;
            }
            else
            {
                var productList = await _dbContext.Products!
                .Include(p => p.Category)
                .Include(p => p.Generic)
                .Include(p => p.Company)
                .Include(p => p.RecommandedPatients)!
                .ThenInclude(r => r.PatientGroup)
                .Where(p => p.CategoryId == categoryId)
                .DefaultIfEmpty()
                .ToListAsync();

                return productList;
            }
            
        }
        catch (Exception)
        {

        }
        return null;
    }

    public async Task Add(Product entity)
    {
        try
        {
            await _dbContext.Products!.AddAsync(entity);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Update(Product obj)
    {
        try
        {
            var product = _dbContext.Products!
                .DefaultIfEmpty()
                .FirstOrDefault(c => c.Id == obj.Id)!;
            //var product = _dbContext.Products!.Where(p => p.Id == obj.Id).FirstOrDefault();

            if (product is null) return;

            product.Name = obj.Name;
            product.Strength = obj.Strength;
            product.Generic = obj.Generic;
            product.Details = obj.Details;
            product.CategoryId = obj.CategoryId;
            product.GenericId = obj.GenericId;
            product.CompanyId = obj.CompanyId;
            product.UpdatedTime = DateTime.Now;
            product.UpdatedById = obj.UpdatedById;
            product.RecommandedPatients = obj.RecommandedPatients;

        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Remove(long id)
    {
        try
        {
            var product = _dbContext.Products!.Where(p => p.Id == id).FirstOrDefault();

            if (product is null) return;

            //_dbContext.Remove(product);
            _dbContext.Products!.Remove(product);
            
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }


  
    /*public async Task<Product?> GetProductByUserId(string id)
    {
        var result = await _dbContext.Products!.Include(u => u.CreatedBy).FirstOrDefaultAsync(u => u.CreatedBy!.Id == id);

        return result;
    }*/

    public void Remove(long productId, long id)
    {
        try
        {
            var rPatient = _dbContext.RecommandedPatient!.FirstOrDefault(i => i.ProductId == productId && i.PatientGroupId == id);
            _dbContext.RecommandedPatient!.Remove(rPatient!);

            //_context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }

    }
}