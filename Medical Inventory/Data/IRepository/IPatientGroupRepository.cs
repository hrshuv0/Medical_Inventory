using Medical_Inventory.Models;

namespace Medical_Inventory.Data.IRepository;


public interface IPatientGroupRepository
{
    Task<PatientGroup?>? GetFirstOrDefault(long id);
    Task<IEnumerable<PatientGroup>?>? GetAll();
    Task Add(PatientGroup entity);
    void Update(PatientGroup obj);
    void Remove(long id);
    Task Save();

    Task<PatientGroup?> GetByName(string name);
    Task<PatientGroup?> GetByName(string name, long id);
}