using DevmasterTrainingManagement.Domain.Entities;
using DevmasterTrainingManagement.Domain.Enums;

namespace DevmasterTrainingManagement.Application.Interfaces;

/// <summary>
/// Service interface for Course management
/// </summary>
public interface ICourseService
{
    /// <summary>
    /// Add a new course
    /// </summary>
    Task AddAsync(Course course);

    /// <summary>
    /// Update an existing course
    /// </summary>
    Task<bool> UpdateAsync(Course course);

    /// <summary>
    /// Delete a course by ID
    /// </summary>
    Task<bool> DeleteAsync(string id);

    /// <summary>
    /// Get a course by ID
    /// </summary>
    Task<Course?> GetByIdAsync(string id);

    /// <summary>
    /// Get all courses
    /// </summary>
    Task<IReadOnlyList<Course>> GetAllAsync();

    /// <summary>
    /// Search courses by keyword (name or description)
    /// </summary>
    Task<IReadOnlyList<Course>> SearchAsync(string keyword);

    /// <summary>
    /// Get courses sorted by a property
    /// </summary>
    Task<IReadOnlyList<Course>> GetSortedAsync(string sortBy = "name", bool ascending = true);

    Task<IReadOnlyList<Course>> GetByStatusAsync(CourseStatus status);
}
