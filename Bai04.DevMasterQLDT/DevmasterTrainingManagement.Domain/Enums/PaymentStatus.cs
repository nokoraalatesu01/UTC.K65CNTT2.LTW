namespace DevmasterTrainingManagement.Domain.Enums;

/// <summary>
/// Enum representing the payment status of an enrollment
/// </summary>
public enum PaymentStatus
{
    Pending = 0,
    PartiallyPaid = 1,
    FullyPaid = 2,
    Overdue = 3
}
