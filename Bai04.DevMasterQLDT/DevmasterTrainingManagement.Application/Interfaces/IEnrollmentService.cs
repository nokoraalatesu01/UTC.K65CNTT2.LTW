using DevmasterTrainingManagement.Domain.Entities;

namespace DevmasterTrainingManagement.Application.Interfaces;

/// <summary>
/// Service interface for Enrollment management
/// </summary>
public interface IEnrollmentService
{
    /// <summary>
    /// Add a new enrollment
    /// </summary>
    Task AddAsync(Enrollment enrollment);

    /// <summary>
    /// Update an existing enrollment
    /// </summary>
    Task<bool> UpdateAsync(Enrollment enrollment);

    /// <summary>
    /// Delete an enrollment by ID
    /// </summary>
    Task<bool> DeleteAsync(string id);

    /// <summary>
    /// Get an enrollment by ID
    /// </summary>
    Task<Enrollment?> GetByIdAsync(string id);

    /// <summary>
    /// Get all enrollments
    /// </summary>
    Task<IReadOnlyList<Enrollment>> GetAllAsync();

    /// <summary>
    /// Get enrollments by student ID
    /// </summary>
    Task<IReadOnlyList<Enrollment>> GetByStudentIdAsync(string studentId);

    /// <summary>
    /// Get enrollments by class ID
    /// </summary>
    Task<IReadOnlyList<Enrollment>> GetByClassIdAsync(string classId);

    /// <summary>
    /// Check if student is already enrolled in a class
    /// </summary>
    Task<bool> IsEnrolledAsync(string studentId, string classId);

    /// <summary>
    /// Record a payment for an enrollment
    /// </summary>
    Task<bool> RecordPaymentAsync(string enrollmentId, decimal amount);

    /// <summary>
    /// Get remaining balance for an enrollment
    /// </summary>
    Task<decimal> GetRemainingBalanceAsync(string enrollmentId);

    Task<bool> CancelAsync(string enrollmentId);
}
