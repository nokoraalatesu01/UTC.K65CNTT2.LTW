using DevmasterTrainingManagement.Application.Interfaces;
using DevmasterTrainingManagement.Domain.Entities;
using DevmasterTrainingManagement.Domain.Enums;

namespace DevmasterTrainingManagement.Application.Services;

/// <summary>
/// Service for managing classes
/// </summary>
public class ClassService : IClassService
{
    private readonly IRepository<Class> _classRepository;
    private readonly IEnrollmentService _enrollmentService;

    public ClassService(IRepository<Class> classRepository, IEnrollmentService enrollmentService)
    {
        _classRepository = classRepository;
        _enrollmentService = enrollmentService;
    }

    public async Task AddAsync(Class @class)
    {
        ArgumentNullException.ThrowIfNull(@class);
        if (string.IsNullOrWhiteSpace(@class.Name) || string.IsNullOrWhiteSpace(@class.CourseId) || @class.MaxStudents <= 0 || @class.EndDate < @class.StartDate)
            throw new ArgumentException("Thông tin lớp học không hợp lệ.");
        if (string.IsNullOrWhiteSpace(@class.Id))
        {
            @class.Id = Guid.NewGuid().ToString();
        }
        
        @class.CreatedDate = DateTime.Now;
        @class.UpdatedDate = DateTime.Now;
        
        await _classRepository.AddAsync(@class);
    }

    public async Task<bool> UpdateAsync(Class @class)
    {
        var existing = await _classRepository.GetByIdAsync(@class.Id);
        if (existing == null)
            return false;

        @class.CreatedDate = existing.CreatedDate;
        @class.UpdatedDate = DateTime.Now;
        
        return await _classRepository.UpdateAsync(@class);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _classRepository.DeleteAsync(id);
    }

    public async Task<Class?> GetByIdAsync(string id)
    {
        return await _classRepository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<Class>> GetAllAsync()
    {
        return await _classRepository.GetAllAsync();
    }

    public async Task<IReadOnlyList<Class>> GetByCourseIdAsync(string courseId)
    {
        var all = await _classRepository.GetAllAsync();
        return all.Where(c => c.CourseId == courseId).ToList();
    }

    public async Task<IReadOnlyList<Class>> GetUpcomingAsync()
    {
        var all = await _classRepository.GetAllAsync();
        return all
            .Where(c => c.StartDate >= DateTime.Now.Date)
            .OrderBy(c => c.StartDate)
            .ToList();
    }

    public async Task<IReadOnlyList<Class>> GetInProgressAsync()
    {
        var all = await _classRepository.GetAllAsync();
        var now = DateTime.Now;
        return all
            .Where(c => c.StartDate <= now && c.EndDate >= now)
            .OrderBy(c => c.StartDate)
            .ToList();
    }

    public async Task<int> GetEnrollmentCountAsync(string classId)
    {
        var enrollments = await _enrollmentService.GetByClassIdAsync(classId);
        return enrollments.Count(e => e.IsActive);
    }

    public async Task<bool> HasAvailableSlotAsync(string classId)
    {
        var @class = await _classRepository.GetByIdAsync(classId);
        if (@class == null)
            return false;

        var count = await GetEnrollmentCountAsync(classId);
        return count < @class.MaxStudents;
    }

    public async Task<IReadOnlyList<Class>> GetByStatusAsync(ClassStatus status)
    {
        var all = await _classRepository.GetAllAsync();
        return all.Where(c => c.Status == status).ToList();
    }

    public async Task<bool> CancelAsync(string classId, ClassStatus newStatus = ClassStatus.Cancelled)
    {
        var @class = await _classRepository.GetByIdAsync(classId);
        if (@class == null)
            return false;

        @class.Status = newStatus;
        @class.UpdatedDate = DateTime.Now;

        return await _classRepository.UpdateAsync(@class);
    }
}
