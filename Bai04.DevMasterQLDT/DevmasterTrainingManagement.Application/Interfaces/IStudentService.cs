using DevmasterTrainingManagement.Domain.Entities;

namespace DevmasterTrainingManagement.Application.Interfaces;

/// <summary>
/// Service interface for Student management
/// </summary>
public interface IStudentService
{
    /// <summary>
    /// Add a new student
    /// </summary>
    Task AddAsync(Student student);

    /// <summary>
    /// Update an existing student
    /// </summary>
    Task<bool> UpdateAsync(Student student);

    /// <summary>
    /// Delete a student by ID
    /// </summary>
    Task<bool> DeleteAsync(string id);

    /// <summary>
    /// Get a student by ID
    /// </summary>
    Task<Student?> GetByIdAsync(string id);

    /// <summary>
    /// Get all students
    /// </summary>
    Task<IReadOnlyList<Student>> GetAllAsync();

    /// <summary>
    /// Search students by full name
    /// </summary>
    Task<IReadOnlyList<Student>> SearchByNameAsync(string name);

    /// <summary>
    /// Search students by phone number
    /// </summary>
    Task<Student?> GetByPhoneAsync(string phone);

    /// <summary>
    /// Search students by email
    /// </summary>
    Task<Student?> GetByEmailAsync(string email);

    /// <summary>
    /// Check if phone number already exists
    /// </summary>
    Task<bool> PhoneExistsAsync(string phone, string? excludeStudentId = null);

    /// <summary>
    /// Check if email already exists
    /// </summary>
    Task<bool> EmailExistsAsync(string email, string? excludeStudentId = null);
}
