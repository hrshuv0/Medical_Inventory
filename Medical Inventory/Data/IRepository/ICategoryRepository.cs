using Medical_Inventory.Models;

namespace Medical_Inventory.Data.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category obj);

}