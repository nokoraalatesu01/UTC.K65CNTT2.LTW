using DevmasterTrainingManagement.Application.Interfaces;
using DevmasterTrainingManagement.Domain.Entities;

namespace DevmasterTrainingManagement.Application.Services;

/// <summary>
/// Service for managing students
/// </summary>
public class StudentService : IStudentService
{
    private readonly IRepository<Student> _repository;

    public StudentService(IRepository<Student> repository)
    {
        _repository = repository;
    }

    public async Task AddAsync(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);
        if (string.IsNullOrWhiteSpace(student.FullName) || string.IsNullOrWhiteSpace(student.Phone))
            throw new ArgumentException("Họ tên và số điện thoại là bắt buộc.");
        if (await PhoneExistsAsync(student.Phone) || (!string.IsNullOrWhiteSpace(student.Email) && await EmailExistsAsync(student.Email)))
            throw new InvalidOperationException("Số điện thoại hoặc email đã tồn tại.");
        if (string.IsNullOrWhiteSpace(student.Id))
        {
            student.Id = Guid.NewGuid().ToString();
        }
        
        student.RegisterDate = DateTime.Now;
        student.CreatedDate = DateTime.Now;
        student.UpdatedDate = DateTime.Now;
        
        await _repository.AddAsync(student);
    }

    public async Task<bool> UpdateAsync(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);
        var existing = await _repository.GetByIdAsync(student.Id);
        if (existing == null)
            return false;
        if (await PhoneExistsAsync(student.Phone, student.Id) || (!string.IsNullOrWhiteSpace(student.Email) && await EmailExistsAsync(student.Email, student.Id)))
            throw new InvalidOperationException("Số điện thoại hoặc email đã tồn tại.");

        student.RegisterDate = existing.RegisterDate;
        student.CreatedDate = existing.CreatedDate;
        student.UpdatedDate = DateTime.Now;
        
        return await _repository.UpdateAsync(student);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<Student?> GetByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<Student>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IReadOnlyList<Student>> SearchByNameAsync(string name)
    {
        var all = await _repository.GetAllAsync();
        return all
            .Where(s => s.FullName.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public async Task<Student?> GetByPhoneAsync(string phone)
    {
        var all = await _repository.GetAllAsync();
        return all.FirstOrDefault(s => s.Phone == phone);
    }

    public async Task<Student?> GetByEmailAsync(string email)
    {
        var all = await _repository.GetAllAsync();
        return all.FirstOrDefault(s => s.Email == email);
    }

    public async Task<bool> PhoneExistsAsync(string phone, string? excludeStudentId = null)
    {
        var student = await GetByPhoneAsync(phone);
        if (student == null)
            return false;

        return excludeStudentId == null || student.Id != excludeStudentId;
    }

    public async Task<bool> EmailExistsAsync(string email, string? excludeStudentId = null)
    {
        var student = await GetByEmailAsync(email);
        if (student == null)
            return false;

        return excludeStudentId == null || student.Id != excludeStudentId;
    }
}
