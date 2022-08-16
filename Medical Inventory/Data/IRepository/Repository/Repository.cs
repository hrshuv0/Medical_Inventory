using System.Linq.Expressions;

namespace Medical_Inventory.Data.IRepository.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    public T? GetFirstOrDefault(Expression<Func<T, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetAll(Expression<Func<T>>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(T entity)
    {
        throw new NotImplementedException();
    }
}