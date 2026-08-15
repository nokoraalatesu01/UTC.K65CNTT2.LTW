namespace DevmasterTrainingManagement.Domain.Entities;

/// <summary>
/// Represents a student/learner
/// </summary>
public class Student
{
    /// <summary>
    /// Unique identifier for the student
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Full name of the student
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Home address
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Date when the student registered
    /// </summary>
    public DateTime RegisterDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Additional notes about the student
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Last update date
    /// </summary>
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}
