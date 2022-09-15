using Medical_Inventory.Models;

namespace Medical_Inventory.Data.IRepository;

public interface IGenericRepository
{
    Task<Generic?>? GetFirstOrDefault(long? id);
    Task<IEnumerable<Generic>?>? GetAll();
    Task Add(Generic entity);
    void Update(Generic entity);
    Task Remove(Generic entity);
    Task<Generic?> GetByName(string name);
    Task<Generic?> GetByName(string name, long id);
    Task Save();
}