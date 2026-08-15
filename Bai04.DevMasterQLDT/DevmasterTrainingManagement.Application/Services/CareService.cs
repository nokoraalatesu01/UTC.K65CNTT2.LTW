using DevmasterTrainingManagement.Application.Interfaces;
using DevmasterTrainingManagement.Domain.Entities;

namespace DevmasterTrainingManagement.Application.Services;

/// <summary>
/// Service for managing care records
/// </summary>
public class CareService : ICareService
{
    private readonly IRepository<CareRecord> _repository;

    public CareService(IRepository<CareRecord> repository)
    {
        _repository = repository;
    }

    public async Task AddAsync(CareRecord careRecord)
    {
        if (string.IsNullOrWhiteSpace(careRecord.Id))
        {
            careRecord.Id = Guid.NewGuid().ToString();
        }
        
        careRecord.CareDate = DateTime.Now;
        careRecord.CreatedDate = DateTime.Now;
        careRecord.UpdatedDate = DateTime.Now;
        
        await _repository.AddAsync(careRecord);
    }

    public async Task<bool> UpdateAsync(CareRecord careRecord)
    {
        var existing = await _repository.GetByIdAsync(careRecord.Id);
        if (existing == null)
            return false;

        careRecord.CareDate = existing.CareDate;
        careRecord.CreatedDate = existing.CreatedDate;
        careRecord.UpdatedDate = DateTime.Now;
        
        return await _repository.UpdateAsync(careRecord);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<CareRecord?> GetByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<CareRecord>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IReadOnlyList<CareRecord>> GetByStudentIdAsync(string studentId)
    {
        var all = await _repository.GetAllAsync();
        return all
            .Where(c => c.StudentId == studentId)
            .OrderByDescending(c => c.CareDate)
            .ToList();
    }

    public async Task<CareRecord?> GetLastCareRecordAsync(string studentId)
    {
        var records = await GetByStudentIdAsync(studentId);
        return records.FirstOrDefault();
    }

    public async Task<IReadOnlyList<CareRecord>> GetTodaysAppointmentsAsync()
    {
        var today = DateTime.Now.Date;
        var all = await _repository.GetAllAsync();
        return all
            .Where(c => c.NextAppointment.HasValue && c.NextAppointment.Value.Date == today)
            .OrderBy(c => c.NextAppointment)
            .ToList();
    }

    public async Task<IReadOnlyList<CareRecord>> GetOverdueAppointmentsAsync()
    {
        var now = DateTime.Now;
        var all = await _repository.GetAllAsync();
        return all
            .Where(c => c.NextAppointment.HasValue && c.NextAppointment.Value < now)
            .OrderBy(c => c.NextAppointment)
            .ToList();
    }
}
