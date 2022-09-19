using Entities;

namespace Inventory.Service.IRepository;

public interface ICategoryRepository
{
    Task<Category?>? GetFirstOrDefault(long id);
    Task<IEnumerable<Category>?>? GetAll();
    Task Add(Category entity);
    void Update(Category obj);
    void Remove(long id);
    Task Save();
    
    Task<Category?> GetByName(string name);
    Task<Category?> GetByName(string name, long id);

}