using DevmasterTrainingManagement.Domain.Enums;

namespace DevmasterTrainingManagement.Domain.Entities;

/// <summary>
/// Represents a training course
/// </summary>
public class Course
{
    /// <summary>
    /// Unique identifier for the course
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Course name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Course fee in currency units
    /// </summary>
    public decimal Fee { get; set; }

    /// <summary>
    /// Course duration in hours
    /// </summary>
    public int Duration { get; set; }

    /// <summary>
    /// Course description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Current status of the course
    /// </summary>
    public CourseStatus Status { get; set; }

    /// <summary>
    /// Creation date of the course
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Last update date
    /// </summary>
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}
