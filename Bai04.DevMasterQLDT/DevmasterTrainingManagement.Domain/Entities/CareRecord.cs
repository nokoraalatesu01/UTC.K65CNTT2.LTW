using DevmasterTrainingManagement.Domain.Enums;

namespace DevmasterTrainingManagement.Domain.Entities;

/// <summary>
/// Represents a care/follow-up record for a student
/// </summary>
public class CareRecord
{
    /// <summary>
    /// Unique identifier for the care record
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Reference to the student
    /// </summary>
    public string StudentId { get; set; } = string.Empty;

    /// <summary>
    /// Date of the care activity
    /// </summary>
    public DateTime CareDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Contact channel used
    /// </summary>
    public ContactChannel ContactChannel { get; set; }

    /// <summary>
    /// Content/summary of the care activity
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Result/outcome of the care activity
    /// </summary>
    public string Result { get; set; } = string.Empty;

    /// <summary>
    /// Date of the next scheduled appointment
    /// </summary>
    public DateTime? NextAppointment { get; set; }

    /// <summary>
    /// Staffer name who did the care
    /// </summary>
    public string CareByStaff { get; set; } = string.Empty;

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Last update date
    /// </summary>
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}
