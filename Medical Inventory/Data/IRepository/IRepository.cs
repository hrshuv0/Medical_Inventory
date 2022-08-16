using System.Linq.Expressions;

namespace Medical_Inventory.Data.IRepository;

public interface IRepository<T> where T: class
{
    Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties=null);
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties=null);
    Task Add(T entity);
    void Remove(T entity);
    Task Save();
}