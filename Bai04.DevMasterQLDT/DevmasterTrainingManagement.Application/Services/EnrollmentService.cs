using DevmasterTrainingManagement.Application.Interfaces;
using DevmasterTrainingManagement.Domain.Entities;
using DevmasterTrainingManagement.Domain.Enums;

namespace DevmasterTrainingManagement.Application.Services;

/// <summary>
/// Service for managing enrollments
/// </summary>
public class EnrollmentService : IEnrollmentService
{
    private readonly IRepository<Enrollment> _repository;
    private readonly IRepository<Student>? _studentRepository;
    private readonly IRepository<Class>? _classRepository;

    public EnrollmentService(IRepository<Enrollment> repository, IRepository<Student>? studentRepository = null, IRepository<Class>? classRepository = null)
    {
        _repository = repository;
        _studentRepository = studentRepository;
        _classRepository = classRepository;
    }

    public async Task AddAsync(Enrollment enrollment)
    {
        ArgumentNullException.ThrowIfNull(enrollment);
        if (enrollment.TotalFee < 0 || enrollment.PaidAmount < 0 || enrollment.PaidAmount > enrollment.TotalFee)
            throw new ArgumentException("Số tiền đăng ký/thanh toán không hợp lệ.");
        if (_studentRepository != null && await _studentRepository.GetByIdAsync(enrollment.StudentId) == null)
            throw new InvalidOperationException("Học viên không tồn tại.");
        if (_classRepository != null)
        {
            var classEntity = await _classRepository.GetByIdAsync(enrollment.ClassId)
                ?? throw new InvalidOperationException("Lớp học không tồn tại.");
            if (classEntity.Status is ClassStatus.Cancelled or ClassStatus.Completed)
                throw new InvalidOperationException("Không thể đăng ký vào lớp đã hủy hoặc đã kết thúc.");
            var existing = await _repository.GetAllAsync();
            if (existing.Any(e => e.StudentId == enrollment.StudentId && e.ClassId == enrollment.ClassId && e.IsActive))
                throw new InvalidOperationException("Học viên đã đăng ký lớp này.");
            if (existing.Count(e => e.ClassId == enrollment.ClassId && e.IsActive) >= classEntity.MaxStudents)
                throw new InvalidOperationException("Lớp đã đủ sĩ số.");
        }
        if (string.IsNullOrWhiteSpace(enrollment.Id))
        {
            enrollment.Id = Guid.NewGuid().ToString();
        }
        
        enrollment.EnrollmentDate = DateTime.Now;
        enrollment.CreatedDate = DateTime.Now;
        enrollment.UpdatedDate = DateTime.Now;
        
        // Determine payment status based on amount
        UpdatePaymentStatus(enrollment);
        
        await _repository.AddAsync(enrollment);
    }

    public async Task<bool> UpdateAsync(Enrollment enrollment)
    {
        var existing = await _repository.GetByIdAsync(enrollment.Id);
        if (existing == null)
            return false;

        enrollment.EnrollmentDate = existing.EnrollmentDate;
        enrollment.CreatedDate = existing.CreatedDate;
        enrollment.UpdatedDate = DateTime.Now;
        
        UpdatePaymentStatus(enrollment);
        
        return await _repository.UpdateAsync(enrollment);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<Enrollment?> GetByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<Enrollment>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IReadOnlyList<Enrollment>> GetByStudentIdAsync(string studentId)
    {
        var all = await _repository.GetAllAsync();
        return all.Where(e => e.StudentId == studentId).ToList();
    }

    public async Task<IReadOnlyList<Enrollment>> GetByClassIdAsync(string classId)
    {
        var all = await _repository.GetAllAsync();
        return all.Where(e => e.ClassId == classId).ToList();
    }

    public async Task<bool> IsEnrolledAsync(string studentId, string classId)
    {
        var all = await _repository.GetAllAsync();
        return all.Any(e => e.StudentId == studentId && e.ClassId == classId && e.IsActive);
    }

    public async Task<bool> RecordPaymentAsync(string enrollmentId, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Số tiền thanh toán phải lớn hơn 0.");
        var enrollment = await _repository.GetByIdAsync(enrollmentId);
        if (enrollment == null)
            return false;
        if (enrollment.PaidAmount + amount > enrollment.TotalFee)
            throw new ArgumentException("Số tiền thanh toán vượt quá học phí.");

        enrollment.PaidAmount += amount;
        enrollment.LastPaymentDate = DateTime.Now;
        UpdatePaymentStatus(enrollment);
        
        return await _repository.UpdateAsync(enrollment);
    }

    public async Task<decimal> GetRemainingBalanceAsync(string enrollmentId)
    {
        var enrollment = await _repository.GetByIdAsync(enrollmentId);
        if (enrollment == null)
            return 0;

        return Math.Max(0, enrollment.TotalFee - enrollment.PaidAmount);
    }

    private void UpdatePaymentStatus(Enrollment enrollment)
    {
        if (enrollment.PaidAmount <= 0)
        {
            enrollment.PaymentStatus = PaymentStatus.Pending;
        }
        else if (enrollment.PaidAmount < enrollment.TotalFee)
        {
            enrollment.PaymentStatus = PaymentStatus.PartiallyPaid;
        }
        else
        {
            enrollment.PaymentStatus = PaymentStatus.FullyPaid;
        }
    }

    public async Task<bool> CancelAsync(string enrollmentId)
    {
        var enrollment = await _repository.GetByIdAsync(enrollmentId);
        if (enrollment == null)
            return false;

        enrollment.IsActive = false;
        enrollment.UpdatedDate = DateTime.Now;

        return await _repository.UpdateAsync(enrollment);
    }
}
