using Medical_Inventory.Models;

namespace Medical_Inventory.Data.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product obj);
    Task<Product?> GetByName(string name);
    Task<Product?> GetProductByUserId(string id);

}