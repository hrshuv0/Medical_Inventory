using Entities;
using Inventory.DAL.DbContext;
using Inventory.Utility.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Service.IRepository.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;
    

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }



    public async Task<Category?>? GetFirstOrDefault(long id)
    {
        try
        {
            var category = await _dbContext.Categories!
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy)
                .FirstOrDefaultAsync(c => c.Id == id)!;

            if(category == null)
                throw new NotFoundException();

            return category;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<IEnumerable<Category>?>? GetAll()
    {
        try
        {
            var categoryList = await _dbContext.Categories!
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy).ToListAsync();

            return categoryList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Add(Category entity)
    {
        try
        {
            await _dbContext.Categories!.AddAsync(entity);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Update(Category obj)
    {
        try
        {
            var category = _dbContext.Categories!.FirstOrDefault(c => c.Id == obj.Id);

            if (category is null)
                //throw new NotFoundException("");
                throw new Exception();

            category.Name = obj.Name;
            category.UpdatedTime = DateTime.Now;
        }
        catch (Exception ex)
        {
            //
        }
    }


    public void Remove(long id)
    {
        try
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
                    if (product.CategoryId == id)
                    {
                        product.Category = null;
                        product.CategoryId = null;
                    }

                }
                _dbContext.SaveChanges();
            }

            _dbContext.Categories!.Remove(category);
            _dbContext.SaveChanges(true);
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
    public async Task<Category?> GetByName(string? name)
    {
        try
        {
            var result = await _dbContext.Categories!.FirstOrDefaultAsync(p => p.Name == name);

            if (result is not null)
                //throw new DuplicationException(name!);
                throw new Exception();

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Category?> GetByName(string name, long id)
    {
        try
        {
            var result = await _dbContext.Categories!.FirstOrDefaultAsync(p => p.Name == name);

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







}