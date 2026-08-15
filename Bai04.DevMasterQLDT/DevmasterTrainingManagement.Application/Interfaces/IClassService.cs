using DevmasterTrainingManagement.Domain.Entities;
using DevmasterTrainingManagement.Domain.Enums;

namespace DevmasterTrainingManagement.Application.Interfaces;

/// <summary>
/// Service interface for Class management
/// </summary>
public interface IClassService
{
    /// <summary>
    /// Add a new class
    /// </summary>
    Task AddAsync(Class @class);

    /// <summary>
    /// Update an existing class
    /// </summary>
    Task<bool> UpdateAsync(Class @class);

    /// <summary>
    /// Delete a class by ID
    /// </summary>
    Task<bool> DeleteAsync(string id);

    /// <summary>
    /// Get a class by ID
    /// </summary>
    Task<Class?> GetByIdAsync(string id);

    /// <summary>
    /// Get all classes
    /// </summary>
    Task<IReadOnlyList<Class>> GetAllAsync();

    /// <summary>
    /// Get classes by course ID
    /// </summary>
    Task<IReadOnlyList<Class>> GetByCourseIdAsync(string courseId);

    /// <summary>
    /// Get upcoming classes (start date >= today)
    /// </summary>
    Task<IReadOnlyList<Class>> GetUpcomingAsync();

    /// <summary>
    /// Get in-progress classes
    /// </summary>
    Task<IReadOnlyList<Class>> GetInProgressAsync();

    /// <summary>
    /// Get current enrollment count for a class
    /// </summary>
    Task<int> GetEnrollmentCountAsync(string classId);

    /// <summary>
    /// Check if class has available slots
    /// </summary>
    Task<bool> HasAvailableSlotAsync(string classId);

    Task<IReadOnlyList<Class>> GetByStatusAsync(ClassStatus status);

    Task<bool> CancelAsync(string classId, ClassStatus newStatus = ClassStatus.Cancelled);
}
