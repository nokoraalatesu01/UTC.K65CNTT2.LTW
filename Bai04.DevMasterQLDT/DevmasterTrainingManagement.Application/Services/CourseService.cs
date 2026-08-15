using DevmasterTrainingManagement.Application.Interfaces;
using DevmasterTrainingManagement.Domain.Entities;
using DevmasterTrainingManagement.Domain.Enums;

namespace DevmasterTrainingManagement.Application.Services;

/// <summary>
/// Service for managing courses
/// </summary>
public class CourseService : ICourseService
{
    private readonly IRepository<Course> _repository;

    public CourseService(IRepository<Course> repository)
    {
        _repository = repository;
    }

    public async Task AddAsync(Course course)
    {
        ArgumentNullException.ThrowIfNull(course);
        if (string.IsNullOrWhiteSpace(course.Name) || course.Fee < 0 || course.Duration <= 0)
            throw new ArgumentException("Tên, học phí và thời lượng khóa học không hợp lệ.");
        if (string.IsNullOrWhiteSpace(course.Id))
        {
            course.Id = Guid.NewGuid().ToString();
        }
        
        course.CreatedDate = DateTime.Now;
        course.UpdatedDate = DateTime.Now;
        
        await _repository.AddAsync(course);
    }

    public async Task<bool> UpdateAsync(Course course)
    {
        var existing = await _repository.GetByIdAsync(course.Id);
        if (existing == null)
            return false;

        course.CreatedDate = existing.CreatedDate;
        course.UpdatedDate = DateTime.Now;
        
        return await _repository.UpdateAsync(course);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<Course?> GetByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<Course>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IReadOnlyList<Course>> SearchAsync(string keyword)
    {
        keyword ??= string.Empty;
        var all = await _repository.GetAllAsync();
        return all
            .Where(c => c.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                       c.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public async Task<IReadOnlyList<Course>> GetSortedAsync(string sortBy = "name", bool ascending = true)
    {
        var all = await _repository.GetAllAsync();
        var sorted = sortBy.ToLower() switch
        {
            "name" => ascending ? all.OrderBy(c => c.Name).ToList() : all.OrderByDescending(c => c.Name).ToList(),
            "fee" => ascending ? all.OrderBy(c => c.Fee).ToList() : all.OrderByDescending(c => c.Fee).ToList(),
            "duration" => ascending ? all.OrderBy(c => c.Duration).ToList() : all.OrderByDescending(c => c.Duration).ToList(),
            "status" => ascending ? all.OrderBy(c => c.Status).ToList() : all.OrderByDescending(c => c.Status).ToList(),
            _ => all.OrderBy(c => c.Name).ToList()
        };
        
        return sorted;
    }

    public async Task<IReadOnlyList<Course>> GetByStatusAsync(CourseStatus status)
    {
        var all = await _repository.GetAllAsync();
        return all.Where(c => c.Status == status).ToList();
    }
}
