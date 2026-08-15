using DevmasterTrainingManagement.Domain.Entities;

namespace DevmasterTrainingManagement.Application.Interfaces;

/// <summary>
/// Service interface for Care record management
/// </summary>
public interface ICareService
{
    /// <summary>
    /// Add a new care record
    /// </summary>
    Task AddAsync(CareRecord careRecord);

    /// <summary>
    /// Update an existing care record
    /// </summary>
    Task<bool> UpdateAsync(CareRecord careRecord);

    /// <summary>
    /// Delete a care record by ID
    /// </summary>
    Task<bool> DeleteAsync(string id);

    /// <summary>
    /// Get a care record by ID
    /// </summary>
    Task<CareRecord?> GetByIdAsync(string id);

    /// <summary>
    /// Get all care records
    /// </summary>
    Task<IReadOnlyList<CareRecord>> GetAllAsync();

    /// <summary>
    /// Get care records by student ID
    /// </summary>
    Task<IReadOnlyList<CareRecord>> GetByStudentIdAsync(string studentId);

    /// <summary>
    /// Get last care record for a student
    /// </summary>
    Task<CareRecord?> GetLastCareRecordAsync(string studentId);

    /// <summary>
    /// Get care records with appointments today
    /// </summary>
    Task<IReadOnlyList<CareRecord>> GetTodaysAppointmentsAsync();

    /// <summary>
    /// Get overdue appointments
    /// </summary>
    Task<IReadOnlyList<CareRecord>> GetOverdueAppointmentsAsync();
}
