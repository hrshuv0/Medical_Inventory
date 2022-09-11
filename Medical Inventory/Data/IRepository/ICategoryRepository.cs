using Medical_Inventory.Models;
using System.Linq.Expressions;

namespace Medical_Inventory.Data.IRepository;

public interface ICategoryRepository
{
    Task<Category?>? GetFirstOrDefault(long id);
    Task<IEnumerable<Category>?>? GetAll();
    Task Add(Category entity);
    void Update(Category obj);
    void Remove(long id);
    Task Save();
    
    Task<Category?> GetByName(string name);

}