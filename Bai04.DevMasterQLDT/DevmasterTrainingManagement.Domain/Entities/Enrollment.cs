using DevmasterTrainingManagement.Domain.Enums;

namespace DevmasterTrainingManagement.Domain.Entities;

/// <summary>
/// Represents an enrollment (registration of a student in a class)
/// </summary>
public class Enrollment
{
    /// <summary>
    /// Unique identifier for the enrollment
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Reference to the student
    /// </summary>
    public string StudentId { get; set; } = string.Empty;

    /// <summary>
    /// Reference to the class
    /// </summary>
    public string ClassId { get; set; } = string.Empty;

    /// <summary>
    /// Date when the student enrolled
    /// </summary>
    public DateTime EnrollmentDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Total fee for this enrollment
    /// </summary>
    public decimal TotalFee { get; set; }

    /// <summary>
    /// Amount already paid
    /// </summary>
    public decimal PaidAmount { get; set; }

    /// <summary>
    /// Payment status
    /// </summary>
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

    /// <summary>
    /// Date of last payment
    /// </summary>
    public DateTime? LastPaymentDate { get; set; }

    /// <summary>
    /// Notes about the enrollment
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Is the enrollment active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Last update date
    /// </summary>
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}
