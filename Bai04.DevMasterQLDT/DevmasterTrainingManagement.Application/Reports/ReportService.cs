using DevmasterTrainingManagement.Application.Interfaces;
using DevmasterTrainingManagement.Domain.Entities;
using DevmasterTrainingManagement.Domain.Enums;

namespace DevmasterTrainingManagement.Application.Reports;

/// <summary>
/// Service for generating business reports using LINQ
/// </summary>
public class ReportService
{
    private readonly ICourseService _courseService;
    private readonly IClassService _classService;
    private readonly IStudentService _studentService;
    private readonly IEnrollmentService _enrollmentService;
    private readonly ICareService _careService;

    public ReportService(
        ICourseService courseService,
        IClassService classService,
        IStudentService studentService,
        IEnrollmentService enrollmentService,
        ICareService careService)
    {
        _courseService = courseService;
        _classService = classService;
        _studentService = studentService;
        _enrollmentService = enrollmentService;
        _careService = careService;
    }

    /// <summary>
    /// Report 1: Count of students by course
    /// </summary>
    public async Task<Dictionary<string, int>> StudentCountByCourseAsync()
    {
        var courses = await _courseService.GetAllAsync();
        var classes = await _classService.GetAllAsync();
        var enrollments = await _enrollmentService.GetAllAsync();

        var report = courses
            .Join(classes, c => c.Id, cl => cl.CourseId, (c, cl) => new { Course = c, Class = cl })
            .Join(enrollments, cc => cc.Class.Id, e => e.ClassId, (cc, e) => new { cc.Course, Enrollment = e })
            .Where(x => x.Enrollment.IsActive)
            .GroupBy(x => x.Course.Name)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.Enrollment.StudentId).Distinct().Count()
            );

        return report;
    }

    /// <summary>
    /// Report 2: Count of students by class
    /// </summary>
    public async Task<Dictionary<string, int>> StudentCountByClassAsync()
    {
        var classes = await _classService.GetAllAsync();
        var enrollments = await _enrollmentService.GetAllAsync();

        var report = classes
            .Join(enrollments, c => c.Id, e => e.ClassId, (c, e) => new { Class = c, Enrollment = e })
            .Where(x => x.Enrollment.IsActive)
            .GroupBy(x => x.Class.Name)
            .ToDictionary(
                g => g.Key,
                g => g.Count()
            );

        return report;
    }

    /// <summary>
    /// Report 3: Upcoming classes (starting soon)
    /// </summary>
    public async Task<List<(string ClassName, DateTime StartDate, int MaxStudents, int CurrentEnrollment)>> UpcomingClassesAsync()
    {
        var upcomingClasses = await _classService.GetUpcomingAsync();
        var enrollments = await _enrollmentService.GetAllAsync();

        var report = upcomingClasses
            .Select(c => new
            {
                c.Name,
                c.StartDate,
                c.MaxStudents,
                CurrentEnrollment = enrollments.Count(e => e.ClassId == c.Id && e.IsActive)
            })
            .OrderBy(x => x.StartDate)
            .Select(x => (x.Name, x.StartDate, x.MaxStudents, x.CurrentEnrollment))
            .ToList();

        return report;
    }

    /// <summary>
    /// Report 4: Students with unpaid/partially paid tuition
    /// </summary>
    public async Task<List<(string StudentName, string ClassName, decimal TotalFee, decimal PaidAmount, decimal Remaining)>> StudentsWithDebtAsync()
    {
        var students = await _studentService.GetAllAsync();
        var classes = await _classService.GetAllAsync();
        var enrollments = await _enrollmentService.GetAllAsync();

        var report = enrollments
            .Where(e => e.IsActive && e.PaymentStatus != PaymentStatus.FullyPaid)
            .Join(students, e => e.StudentId, s => s.Id, (e, s) => new { Enrollment = e, Student = s })
            .Join(classes, es => es.Enrollment.ClassId, c => c.Id, (es, c) => new { es.Enrollment, es.Student, Class = c })
            .Select(x => (
                StudentName: x.Student.FullName,
                ClassName: x.Class.Name,
                TotalFee: x.Enrollment.TotalFee,
                PaidAmount: x.Enrollment.PaidAmount,
                Remaining: x.Enrollment.TotalFee - x.Enrollment.PaidAmount
            ))
            .OrderByDescending(x => x.Remaining)
            .ToList();

        return report;
    }

    /// <summary>
    /// Report 5: Total revenue
    /// </summary>
    public async Task<decimal> TotalRevenueAsync()
    {
        var enrollments = await _enrollmentService.GetAllAsync();
        
        var totalRevenue = enrollments
            .Where(e => e.PaymentStatus == PaymentStatus.FullyPaid)
            .Sum(e => e.PaidAmount);

        return totalRevenue;
    }

    /// <summary>
    /// Report 6: Revenue by month
    /// </summary>
    public async Task<Dictionary<string, decimal>> RevenueByMonthAsync()
    {
        var enrollments = await _enrollmentService.GetAllAsync();

        var report = enrollments
            .Where(e => e.LastPaymentDate.HasValue && e.PaidAmount > 0)
            .GroupBy(e => e.LastPaymentDate!.Value.ToString("yyyy-MM"))
            .ToDictionary(
                g => g.Key,
                g => g.Sum(e => e.PaidAmount)
            );

        return report.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
    }

    /// <summary>
    /// Report 7: Course with most students
    /// </summary>
    public async Task<(string CourseName, int StudentCount)?> CourseWithMostStudentsAsync()
    {
        var studentCountByCourse = await StudentCountByCourseAsync();
        
        if (studentCountByCourse.Count == 0)
            return null;

        var topCourse = studentCountByCourse.OrderByDescending(x => x.Value).First();
        return (topCourse.Key, topCourse.Value);
    }

    /// <summary>
    /// Report 8: Students with appointments today
    /// </summary>
    public async Task<List<(string StudentName, string Content, DateTime? NextAppointment)>> TodaysAppointmentsAsync()
    {
        var todaysAppointments = await _careService.GetTodaysAppointmentsAsync();
        var students = await _studentService.GetAllAsync();

        var report = todaysAppointments
            .Join(students, c => c.StudentId, s => s.Id, (c, s) => new { Care = c, Student = s })
            .Select(x => (
                StudentName: x.Student.FullName,
                Content: x.Care.Content,
                NextAppointment: x.Care.NextAppointment
            ))
            .OrderBy(x => x.NextAppointment)
            .ToList();

        return report;
    }

    /// <summary>
    /// Report 9: Students neglected in care (longest without care)
    /// </summary>
    public async Task<List<(string StudentName, DateTime? LastCareDate, int DaysSinceCare)>> NeglectedStudentsAsync()
    {
        var students = await _studentService.GetAllAsync();
        var careRecords = await _careService.GetAllAsync();

        var report = students
            .GroupJoin(careRecords, s => s.Id, c => c.StudentId, (s, cares) => new
            {
                Student = s,
                LastCare = cares.OrderByDescending(c => c.CareDate).FirstOrDefault()
            })
            .Select(x => new
            {
                StudentName = x.Student.FullName,
                LastCareDate = x.LastCare?.CareDate,
                DaysSinceCare = x.LastCare == null 
                    ? (int)(DateTime.Now - x.Student.CreatedDate).TotalDays 
                    : (int)(DateTime.Now - x.LastCare.CareDate).TotalDays
            })
            .OrderByDescending(x => x.DaysSinceCare)
            .Select(x => (x.StudentName, x.LastCareDate, x.DaysSinceCare))
            .ToList();

        return report;
    }

    /// <summary>
    /// Report 10: Payment completion rate
    /// </summary>
    public async Task<(int TotalEnrollments, int FullyPaid, int PartiallyPaid, int Pending, decimal CompletionPercentage)> PaymentCompletionRateAsync()
    {
        var enrollments = await _enrollmentService.GetAllAsync();
        var activeEnrollments = enrollments.Where(e => e.IsActive).ToList();

        var totalEnrollments = activeEnrollments.Count;
        var fullyPaid = activeEnrollments.Count(e => e.PaymentStatus == PaymentStatus.FullyPaid);
        var partiallyPaid = activeEnrollments.Count(e => e.PaymentStatus == PaymentStatus.PartiallyPaid);
        var pending = activeEnrollments.Count(e => e.PaymentStatus == PaymentStatus.Pending);

        var completionPercentage = totalEnrollments > 0 
            ? Math.Round((decimal)fullyPaid / totalEnrollments * 100, 2) 
            : 0;

        return (totalEnrollments, fullyPaid, partiallyPaid, pending, completionPercentage);
    }

    /// <summary>
    /// Report 11: Classes by status
    /// </summary>
    public async Task<Dictionary<ClassStatus, int>> ClassesByStatusAsync()
    {
        var classes = await _classService.GetAllAsync();

        var report = classes
            .GroupBy(c => c.Status)
            .ToDictionary(g => g.Key, g => g.Count());

        return report;
    }

    /// <summary>
    /// Report 12: Average class occupancy
    /// </summary>
    public async Task<Dictionary<string, (int MaxStudents, int CurrentStudents, decimal OccupancyPercentage)>> ClassOccupancyAsync()
    {
        var classes = await _classService.GetAllAsync();
        var enrollments = await _enrollmentService.GetAllAsync();

        var report = classes
            .Select(c => new
            {
                ClassName = c.Name,
                MaxStudents = c.MaxStudents,
                CurrentStudents = enrollments.Count(e => e.ClassId == c.Id && e.IsActive)
            })
            .ToDictionary(
                x => x.ClassName,
                x => (
                    MaxStudents: x.MaxStudents,
                    CurrentStudents: x.CurrentStudents,
                    OccupancyPercentage: x.MaxStudents > 0 
                        ? Math.Round((decimal)x.CurrentStudents / x.MaxStudents * 100, 2) 
                        : 0
                )
            );

        return report;
    }

    /// <summary>
    /// Báo cáo 13: Thống kê học phí theo khóa học
    /// </summary>
    public async Task<Dictionary<string, decimal>> CourseFeeStatisticsAsync()
    {
        var courses = await _courseService.GetAllAsync();
        return courses.ToDictionary(
            c => c.Name,
            c => c.Fee
        );
    }

    /// <summary>
    /// Báo cáo 14: Tổng học phí thu được
    /// </summary>
    public async Task<decimal> TotalCourseFeeAsync()
    {
        var courses = await _courseService.GetAllAsync();
        return courses.Sum(c => c.Fee);
    }

    /// <summary>
    /// Báo cáo 15: Doanh thu tiềm năng (học phí theo số lớp)
    /// </summary>
    public async Task<Dictionary<string, decimal>> PotentialRevenueByCourseAsync()
    {
        var courses = await _courseService.GetAllAsync();
        var report = new Dictionary<string, decimal>();

        foreach (var course in courses)
        {
            var classes = await _classService.GetByCourseIdAsync(course.Id);
            var potentialRevenue = course.Fee * classes.Count();
            report[course.Name] = potentialRevenue;
        }

        return report;
    }

    [Obsolete("Use PotentialRevenueByCourseAsync")]
    public Task<Dictionary<string, decimal>> PotentialRevenueByCoursAsync() => PotentialRevenueByCourseAsync();
}
