using Medical_Inventory.Models;

namespace Medical_Inventory.Data.IRepository;

public interface ICompanyRepository
{
    Task<Company?>? GetFirstOrDefault(long? id);
    Task<IEnumerable<Company>?>? GetAll();
    Task Add(Company entity);
    void Update(Company entity);
    Task Remove(Company entity);
    Task<Company?> GetByName(string? name);
    Task<Company?> GetByName(string name, long id);
    Task Save();
}