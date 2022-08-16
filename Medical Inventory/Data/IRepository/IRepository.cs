using System.Linq.Expressions;

namespace Medical_Inventory.Data.IRepository;

public interface IRepository<T> where T: class
{
    T? GetFirstOrDefault(Expression<Func<T, bool>> filter);
    IEnumerable<T> GetAll(Expression<Func<T>>? filter = null);
    void Add(T entity);
    void Remove(T entity);
}