using Medical_Inventory.Models;

namespace Medical_Inventory.Data.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category obj);
    void Remove(int id);
    Task<Category?> GetByName(string name);

}