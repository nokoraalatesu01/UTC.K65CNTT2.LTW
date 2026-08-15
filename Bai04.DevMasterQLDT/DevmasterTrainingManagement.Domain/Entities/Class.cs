using DevmasterTrainingManagement.Domain.Enums;

namespace DevmasterTrainingManagement.Domain.Entities;

/// <summary>
/// Represents a training class
/// </summary>
public class Class
{
    /// <summary>
    /// Unique identifier for the class
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Class name/code
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Reference to the course this class belongs to
    /// </summary>
    public string CourseId { get; set; } = string.Empty;

    /// <summary>
    /// Start date of the class
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// End date of the class
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Schedule description (e.g., "Mon-Wed-Fri 18:00-20:00")
    /// </summary>
    public string Schedule { get; set; } = string.Empty;

    /// <summary>
    /// Maximum number of students allowed in this class
    /// </summary>
    public int MaxStudents { get; set; }

    /// <summary>
    /// Current status of the class
    /// </summary>
    public ClassStatus Status { get; set; }

    /// <summary>
    /// Class location/room number
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Instructor name
    /// </summary>
    public string InstructorName { get; set; } = string.Empty;

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Last update date
    /// </summary>
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}
