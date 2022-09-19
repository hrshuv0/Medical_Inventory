using Inventory.DAL.DbContext;

namespace Medical_Inventory.Data.IRepository.Repository;

public class RecommendedPatientRepository : IRecommendedPatientRepository
{
    private readonly ApplicationDbContext _context;

    public RecommendedPatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public void Remove(long productId, long id)
    {
        try
        {
            var rPatient = _context.RecommandedPatient!.FirstOrDefault(i => i.ProductId == productId && i.PatientGroupId == id);
            _context.RecommandedPatient!.Remove(rPatient!);

            //_context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
        
    }
}