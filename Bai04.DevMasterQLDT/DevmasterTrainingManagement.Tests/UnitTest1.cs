using DevmasterTrainingManagement.Application.Services;
using DevmasterTrainingManagement.Domain.Entities;
using DevmasterTrainingManagement.Domain.Enums;
using DevmasterTrainingManagement.Infrastructure.Repositories;

namespace DevmasterTrainingManagement.Tests;

public class CourseServiceTests
{
    private readonly JsonRepository<Course> _repository;
    private readonly CourseService _courseService;
    private readonly string _testDataDirectory;

    public CourseServiceTests()
    {
        // Use a test directory
        _testDataDirectory = Path.Combine(Path.GetTempPath(), "TestData_" + Guid.NewGuid());
        Directory.CreateDirectory(_testDataDirectory);
        
        _repository = new JsonRepository<Course>(_testDataDirectory);
        _courseService = new CourseService(_repository);
    }

    [Fact]
    public async Task AddCourse_ShouldSucceed()
    {
        // Arrange
        var course = new Course
        {
            Id = Guid.NewGuid().ToString(),
            Name = "C# Advanced",
            Fee = 1500,
            Duration = 40,
            Description = "Advanced C# course",
            Status = CourseStatus.Open
        };

        // Act
        await _courseService.AddAsync(course);
        var retrieved = await _courseService.GetByIdAsync(course.Id);

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal("C# Advanced", retrieved.Name);
        Assert.Equal(1500, retrieved.Fee);
    }

    [Fact]
    public async Task SearchCourse_ByKeyword_ShouldReturnResults()
    {
        // Arrange
        var course1 = new Course 
        { 
            Id = Guid.NewGuid().ToString(),
            Name = "Python Basics", 
            Fee = 1000,
            Duration = 30,
            Description = "Learn Python fundamentals",
            Status = CourseStatus.Open
        };
        
        var course2 = new Course 
        { 
            Id = Guid.NewGuid().ToString(),
            Name = "Java Programming", 
            Fee = 1200,
            Duration = 35,
            Description = "Learn Java",
            Status = CourseStatus.Open
        };

        await _courseService.AddAsync(course1);
        await _courseService.AddAsync(course2);

        // Act
        var results = await _courseService.SearchAsync("Python");

        // Assert
        Assert.Single(results);
        Assert.Equal("Python Basics", results[0].Name);
    }

    [Fact]
    public async Task DeleteCourse_ShouldSucceed()
    {
        // Arrange
        var course = new Course
        {
            Id = Guid.NewGuid().ToString(),
            Name = "To Delete",
            Fee = 500,
            Duration = 20,
            Description = "Test course",
            Status = CourseStatus.Draft
        };

        await _courseService.AddAsync(course);

        // Act
        var deleted = await _courseService.DeleteAsync(course.Id);
        var retrieved = await _courseService.GetByIdAsync(course.Id);

        // Assert
        Assert.True(deleted);
        Assert.Null(retrieved);
    }
}

public class StudentServiceTests
{
    private readonly JsonRepository<Student> _repository;
    private readonly StudentService _studentService;
    private readonly string _testDataDirectory;

    public StudentServiceTests()
    {
        _testDataDirectory = Path.Combine(Path.GetTempPath(), "TestData_" + Guid.NewGuid());
        Directory.CreateDirectory(_testDataDirectory);
        
        _repository = new JsonRepository<Student>(_testDataDirectory);
        _studentService = new StudentService(_repository);
    }

    [Fact]
    public async Task AddStudent_WithValidData_ShouldSucceed()
    {
        // Arrange
        var student = new Student
        {
            Id = Guid.NewGuid().ToString(),
            FullName = "Nguyen Van A",
            Phone = "0912345678",
            Email = "a@example.com",
            Address = "123 Main St",
            DateOfBirth = new DateTime(2000, 1, 15)
        };

        // Act
        await _studentService.AddAsync(student);
        var retrieved = await _studentService.GetByIdAsync(student.Id);

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal("Nguyen Van A", retrieved.FullName);
        Assert.Equal("0912345678", retrieved.Phone);
    }

    [Fact]
    public async Task PhoneExists_WithDuplicatePhone_ShouldReturnTrue()
    {
        // Arrange
        var student1 = new Student
        {
            Id = Guid.NewGuid().ToString(),
            FullName = "Student 1",
            Phone = "0987654321",
            Email = "s1@example.com",
            DateOfBirth = new DateTime(2000, 1, 1)
        };

        var student2 = new Student
        {
            Id = Guid.NewGuid().ToString(),
            FullName = "Student 2",
            Phone = "0987654321", // Same phone
            Email = "s2@example.com",
            DateOfBirth = new DateTime(2000, 2, 2)
        };

        // Act
        await _studentService.AddAsync(student1);
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _studentService.AddAsync(student2));

        // Assert: duplicate phone numbers are rejected by the business rule.
        Assert.Contains("đã tồn tại", exception.Message);
    }

    [Fact]
    public async Task GetByPhone_ShouldReturnStudent()
    {
        // Arrange
        var student = new Student
        {
            Id = Guid.NewGuid().ToString(),
            FullName = "Test Student",
            Phone = "0123456789",
            Email = "test@example.com",
            DateOfBirth = new DateTime(2001, 1, 1)
        };

        await _studentService.AddAsync(student);

        // Act
        var retrieved = await _studentService.GetByPhoneAsync("0123456789");

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal("Test Student", retrieved.FullName);
    }
}

public class EnrollmentServiceTests
{
    private readonly JsonRepository<Enrollment> _repository;
    private readonly EnrollmentService _enrollmentService;
    private readonly string _testDataDirectory;

    public EnrollmentServiceTests()
    {
        _testDataDirectory = Path.Combine(Path.GetTempPath(), "TestData_" + Guid.NewGuid());
        Directory.CreateDirectory(_testDataDirectory);
        
        _repository = new JsonRepository<Enrollment>(_testDataDirectory);
        _enrollmentService = new EnrollmentService(_repository);
    }

    [Fact]
    public async Task CreateEnrollment_ShouldSetCorrectPaymentStatus()
    {
        // Arrange
        var enrollment = new Enrollment
        {
            Id = Guid.NewGuid().ToString(),
            StudentId = Guid.NewGuid().ToString(),
            ClassId = Guid.NewGuid().ToString(),
            TotalFee = 1000,
            PaidAmount = 0
        };

        // Act
        await _enrollmentService.AddAsync(enrollment);
        var retrieved = await _enrollmentService.GetByIdAsync(enrollment.Id);

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal(PaymentStatus.Pending, retrieved.PaymentStatus);
    }

    [Fact]
    public async Task RecordPayment_ShouldUpdatePaidAmount()
    {
        // Arrange
        var enrollment = new Enrollment
        {
            Id = Guid.NewGuid().ToString(),
            StudentId = Guid.NewGuid().ToString(),
            ClassId = Guid.NewGuid().ToString(),
            TotalFee = 1000,
            PaidAmount = 0
        };

        await _enrollmentService.AddAsync(enrollment);

        // Act
        await _enrollmentService.RecordPaymentAsync(enrollment.Id, 500);
        var retrieved = await _enrollmentService.GetByIdAsync(enrollment.Id);

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal(500, retrieved.PaidAmount);
        Assert.Equal(PaymentStatus.PartiallyPaid, retrieved.PaymentStatus);
    }

    [Fact]
    public async Task GetRemainingBalance_ShouldCalculateCorrectly()
    {
        // Arrange
        var enrollment = new Enrollment
        {
            Id = Guid.NewGuid().ToString(),
            StudentId = Guid.NewGuid().ToString(),
            ClassId = Guid.NewGuid().ToString(),
            TotalFee = 1000,
            PaidAmount = 300
        };

        await _enrollmentService.AddAsync(enrollment);

        // Act
        var balance = await _enrollmentService.GetRemainingBalanceAsync(enrollment.Id);

        // Assert
        Assert.Equal(700, balance);
    }
}

public class BackupServiceTests
{
    [Fact]
    public void CreateBackup_ShouldCreateBackupDirectory()
    {
        // Arrange
        var dataDirectory = Path.Combine(Path.GetTempPath(), "Data_" + Guid.NewGuid());
        Directory.CreateDirectory(dataDirectory);
        
        // Create a dummy JSON file
        File.WriteAllText(Path.Combine(dataDirectory, "courses.json"), "[]");

        var logger = new Infrastructure.Logging.FileLogger(Path.GetTempPath());
        var backupService = new Infrastructure.Persistence.BackupService(dataDirectory, logger);

        // Act
        backupService.CreateBackup();
        var backups = backupService.GetAvailableBackups();

        // Assert
        Assert.NotEmpty(backups);
    }
}
