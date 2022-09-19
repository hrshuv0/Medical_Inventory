using Entities;

namespace Inventory.Service.IRepository;

public interface IProductRepository
{
    Task<Product?>? GetFirstOrDefault(long id);
    Task<List<Product>?>? GetAll(long? categoryId = null);
    Task Add(Product entity);
    void Update(Product obj);
    void Remove(long id);
    void Save();

    Task<Product?> GetByName(string? name);
    Task<Product?> GetByName(string name, long id);
    void Remove(long productId, long id);
    
}