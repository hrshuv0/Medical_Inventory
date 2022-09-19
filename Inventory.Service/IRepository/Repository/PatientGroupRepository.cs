using Entities;
using Inventory.DAL.DbContext;
using Inventory.Utility.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Service.IRepository.Repository;


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
            var pGroup = await _dbContext.PatientGroup!
                .Include(p => p.CreatedBy)
                .Include(p => p.UpdatedBy)
                .ToListAsync();

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

    public async Task<PatientGroup?> GetByName(string name, long id)
    {
        try
        {
            var result = await _dbContext.PatientGroup!.FirstOrDefaultAsync(p => p.Name == name && p.Id == id);

            if(result is not null)
            {
                if(result.Id != id)
                {
                    throw new DuplicationException(name!);
                }
            }
            return result;
        }
        catch(Exception)
        {
            throw;
        }
    }

    public async Task<PatientGroup?>? GetFirstOrDefault(long id)
    {
        try
        {
            var pGroup = await _dbContext.PatientGroup!
                .Include(p => p.CreatedBy)
                .Include(p => p.UpdatedBy)
                .DefaultIfEmpty().FirstOrDefaultAsync(c => c.Id == id)!;

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

            _dbContext.PatientGroup!.Remove(entity!);

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
            pGroup.UpdatedTime = DateTime.Now;
            pGroup.UpdatedById = obj.UpdatedById;
        }
        catch (Exception)
        {
            //
        }
    }
}