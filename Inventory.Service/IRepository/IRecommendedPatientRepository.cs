namespace Inventory.Service.IRepository;

public interface IRecommendedPatientRepository
{
    void Remove(long productId, long id);
}