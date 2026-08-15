using System.Globalization;
using CsvHelper;
using DevmasterTrainingManagement.Domain.Entities;
using DevmasterTrainingManagement.Infrastructure.Logging;

namespace DevmasterTrainingManagement.Infrastructure.Csv;

/// <summary>
/// Service for handling CSV import/export of students
/// </summary>
public class StudentCsvService
{
    private readonly FileLogger _logger;
    private const string CsvHeader = "Id,FullName,DateOfBirth,Phone,Email,Address,RegisterDate,Notes";

    public StudentCsvService(FileLogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Export students to CSV file
    /// </summary>
    public async Task ExportToCsvAsync(List<Student> students, string filePath)
    {
        try
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                await csv.WriteRecordsAsync(students.Select(s => new
                {
                    s.Id,
                    s.FullName,
                    DateOfBirth = s.DateOfBirth.ToString("yyyy-MM-dd"),
                    s.Phone,
                    s.Email,
                    s.Address,
                    RegisterDate = s.RegisterDate.ToString("yyyy-MM-dd"),
                    s.Notes
                }));
            }

            _logger.LogInfo($"Successfully exported {students.Count} students to {filePath}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error exporting students to CSV: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// Import students from CSV file
    /// </summary>
    public async Task<List<Student>> ImportFromCsvAsync(string filePath)
    {
        var students = new List<Student>();
        var errors = new List<string>();

        try
        {
            if (!File.Exists(filePath))
            {
                _logger.LogWarning($"CSV file not found: {filePath}");
                return students;
            }

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecordsAsync<dynamic>();
                var rowNum = 1;

                await foreach (var record in records)
                {
                    rowNum++;
                    try
                    {
                        var student = ParseCsvRecord(record);
                        if (student != null)
                            students.Add(student);
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Row {rowNum}: {ex.Message}");
                        _logger.LogWarning($"Error parsing row {rowNum}: {ex.Message}");
                    }
                }
            }

            _logger.LogInfo($"Successfully imported {students.Count} students from {filePath}");
            if (errors.Count > 0)
                _logger.LogWarning($"Import completed with {errors.Count} errors");

            return students;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error importing students from CSV: {ex.Message}", ex);
            throw;
        }
    }

    private Student? ParseCsvRecord(dynamic record)
    {
        try
        {
            var student = new Student
            {
                Id = string.IsNullOrEmpty(record.Id) ? Guid.NewGuid().ToString() : record.Id,
                FullName = record.FullName ?? throw new InvalidOperationException("FullName is required"),
                Phone = record.Phone ?? throw new InvalidOperationException("Phone is required"),
                Email = record.Email ?? string.Empty,
                Address = record.Address ?? string.Empty,
                Notes = record.Notes ?? string.Empty
            };

            // Parse date of birth
            if (!string.IsNullOrEmpty(record.DateOfBirth))
            {
                if (DateTime.TryParseExact(record.DateOfBirth, "yyyy-MM-dd", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
                {
                    student.DateOfBirth = dob;
                }
                else
                {
                    throw new InvalidOperationException($"Invalid date format for DateOfBirth: {record.DateOfBirth}");
                }
            }

            // Parse register date
            if (!string.IsNullOrEmpty(record.RegisterDate))
            {
                if (DateTime.TryParseExact(record.RegisterDate, "yyyy-MM-dd", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime regDate))
                {
                    student.RegisterDate = regDate;
                }
            }

            return student;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to parse CSV record: {ex.Message}", ex);
        }
    }
}
