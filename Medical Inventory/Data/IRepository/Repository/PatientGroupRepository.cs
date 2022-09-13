using Medical_Inventory.Exceptions;
using Medical_Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_Inventory.Data.IRepository.Repository;


public class PatientGroupRepository : IPatientGroupRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PatientGroupRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(PatientGroup entity)
    {
        try
        {
            await _dbContext.PatientGroup!.AddAsync(entity);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<PatientGroup>?>? GetAll()
    {
        try
        {
            var pGroup = await _dbContext.PatientGroup!.ToListAsync();

            return pGroup;
        }
        catch (Exception)
        {

        }
        return null;
    }

    public async Task<PatientGroup?> GetByName(string name)
    {
        try
        {
            var result = await _dbContext.PatientGroup!.FirstOrDefaultAsync(p => p.Name == name);

            if (result is not null)
                throw new DuplicationException(name!);

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PatientGroup?>? GetFirstOrDefault(long id)
    {
        try
        {
            var pGroup = await _dbContext.PatientGroup!.FirstOrDefaultAsync(c => c.Id == id)!;

            return pGroup;
        }
        catch (Exception)
        {
            // ignored
        }

        return null;
    }

    public void Remove(long id)
    {
        try
        {
            var entity = _dbContext.PatientGroup!.FirstOrDefault(p => p.Id == id);

            _dbContext.Remove(entity);

        }
        catch (Exception)
        {

        }
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Update(PatientGroup obj)
    {
        try
        {
            var pGroup = _dbContext.PatientGroup!.FirstOrDefault(c => c.Id == obj.Id);

            if (pGroup is null)
                throw new NotFoundException("");

            pGroup.Name = obj.Name;
        }
        catch (Exception)
        {
            //
        }
    }
}