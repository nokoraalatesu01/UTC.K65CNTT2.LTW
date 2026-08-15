using DevmasterTrainingManagement.Application.Interfaces;
using DevmasterTrainingManagement.Application.Reports;
using DevmasterTrainingManagement.Domain.Entities;
using DevmasterTrainingManagement.Domain.Enums;
using DevmasterTrainingManagement.Infrastructure.Csv;
using DevmasterTrainingManagement.Infrastructure.Logging;
using DevmasterTrainingManagement.Infrastructure.Persistence;

namespace DevmasterTrainingManagement.ConsoleUI.Menus;

/// <summary>Console UI for all required training-management workflows.</summary>
public sealed class MainMenu
{
    private readonly ICourseService _courses; private readonly IClassService _classes; private readonly IStudentService _students;
    private readonly IEnrollmentService _enrollments; private readonly ICareService _care; private readonly ReportService _reports;
    private readonly BackupService _backups; private readonly StudentCsvService _csv; private readonly FileLogger _logger;

    public MainMenu(ICourseService courses, IClassService classes, IStudentService students, IEnrollmentService enrollments,
        ICareService care, ReportService reports, BackupService backups, StudentCsvService csv, FileLogger logger)
    { _courses = courses; _classes = classes; _students = students; _enrollments = enrollments; _care = care; _reports = reports; _backups = backups; _csv = csv; _logger = logger; }

    public void Run()
    {
        while (true)
        {
            Console.Clear(); Console.WriteLine("=== DEVMASTER TRAINING MANAGEMENT ===");
            Console.WriteLine("1 Khoa hoc | 2 Lop hoc | 3 Hoc vien | 4 Dang ky | 5 Cham soc | 6 Bao cao | 7 CSV | 8 Sao luu | 0 Thoat");
            switch (Read("Chon: ")) { case "1": Courses(); break; case "2": Classes(); break; case "3": Students(); break; case "4": Enrollments(); break; case "5": Care(); break; case "6": Reports(); break; case "7": Csv(); break; case "8": Backups(); break; case "0": _logger.LogInfo("Application ended normally"); return; }
        }
    }

    private void Courses()
    {
        while (true)
        {
            Console.Clear(); Console.WriteLine("--- KHOA HOC --- 1 Them 2 Sua 3 Xoa 4 Xem 5 Tim 6 Sap xep 7 Loc trang thai 0 Quay lai"); var c = Read("Chon: "); if (c == "0") return;
            try
            {
                if (c == "1") { var x = new Course { Name = Read("Ten: "), Fee = Decimal("Hoc phi: "), Duration = Int("Thoi luong (gio): "), Description = Read("Mo ta: "), Status = CourseStatus.Open }; _courses.AddAsync(x).GetAwaiter().GetResult(); }
                else if (c == "2") { var x = FindCourse(); if (x != null) { x.Name = ReadOr("Ten", x.Name); x.Fee = DecimalOr("Hoc phi", x.Fee); x.Duration = IntOr("Thoi luong", x.Duration); x.Description = ReadOr("Mo ta", x.Description); _courses.UpdateAsync(x).GetAwaiter().GetResult(); } }
                else if (c == "3") Console.WriteLine(_courses.DeleteAsync(Read("ID: ")).GetAwaiter().GetResult() ? "Da xoa." : "Khong tim thay.");
                else if (c == "4") { foreach (var x in _courses.GetAllAsync().GetAwaiter().GetResult()) Print(x); Pause(); }
                else if (c == "5") { foreach (var x in _courses.SearchAsync(Read("Tu khoa: ")).GetAwaiter().GetResult()) Print(x); Pause(); }
                else if (c == "6") { var key = Read("name/fee/duration/status: "); var asc = Read("Tang dan? (y/n): ").ToLower() != "n"; foreach (var x in _courses.GetSortedAsync(key, asc).GetAwaiter().GetResult()) Print(x); Pause(); }
                else if (c == "7") { foreach (var x in _courses.GetByStatusAsync((CourseStatus)Int("0 Draft, 1 Open, 2 Closed: ")).GetAwaiter().GetResult()) Print(x); Pause(); }
            } catch (Exception ex) { Error(ex); }
        }
    }

    private void Classes()
    {
        while (true)
        {
            Console.Clear(); Console.WriteLine("--- LOP HOC --- 1 Tao 2 Cap nhat 3 Xoa 4 Tat ca 5 Sap khai giang 6 Dang hoc 7 Si so 8 Dong/Huy 0 Quay lai"); var c = Read("Chon: "); if (c == "0") return;
            try
            {
                if (c == "1") { var x = new Class { Name = Read("Ten lop: "), CourseId = Read("Course ID: "), StartDate = Date("Bat dau: "), EndDate = Date("Ket thuc: "), Schedule = Read("Lich hoc: "), MaxStudents = Int("Si so toi da: "), Status = ClassStatus.Scheduled }; _classes.AddAsync(x).GetAwaiter().GetResult(); }
                else if (c == "2") { var x = FindClass(); if (x != null) { x.Name = ReadOr("Ten lop", x.Name); x.Schedule = ReadOr("Lich hoc", x.Schedule); x.MaxStudents = IntOr("Si so", x.MaxStudents); _classes.UpdateAsync(x).GetAwaiter().GetResult(); } }
                else if (c == "3") Console.WriteLine(_classes.DeleteAsync(Read("ID: ")).GetAwaiter().GetResult() ? "Da xoa." : "Khong tim thay.");
                else if (c == "4") PrintClasses(_classes.GetAllAsync().GetAwaiter().GetResult());
                else if (c == "5") PrintClasses(_classes.GetUpcomingAsync().GetAwaiter().GetResult());
                else if (c == "6") PrintClasses(_classes.GetInProgressAsync().GetAwaiter().GetResult());
                else if (c == "7") { var id = Read("Class ID: "); Console.WriteLine($"Si so: {_classes.GetEnrollmentCountAsync(id).GetAwaiter().GetResult()}; Con cho: {_classes.HasAvailableSlotAsync(id).GetAwaiter().GetResult()}"); }
                else if (c == "8") Console.WriteLine(_classes.CancelAsync(Read("Class ID: ")).GetAwaiter().GetResult() ? "Da huy/dong lop." : "Khong tim thay.");
                Pause();
            } catch (Exception ex) { Error(ex); }
        }
    }

    private void Students()
    {
        while (true)
        {
            Console.Clear(); Console.WriteLine("--- HOC VIEN --- 1 Them 2 Sua 3 Xoa 4 Tat ca 5 Tim ten 6 Tim dien thoai/email 0 Quay lai"); var c = Read("Chon: "); if (c == "0") return;
            try
            {
                if (c == "1") { var x = new Student { FullName = Read("Ho ten: "), DateOfBirth = Date("Ngay sinh: "), Phone = Read("Dien thoai: "), Email = Read("Email: "), Address = Read("Dia chi: ") }; _students.AddAsync(x).GetAwaiter().GetResult(); }
                else if (c == "2") { var x = StudentById(); if (x != null) { x.FullName = ReadOr("Ho ten", x.FullName); x.Phone = ReadOr("Dien thoai", x.Phone); x.Email = ReadOr("Email", x.Email); x.Address = ReadOr("Dia chi", x.Address); _students.UpdateAsync(x).GetAwaiter().GetResult(); } }
                else if (c == "3") Console.WriteLine(_students.DeleteAsync(Read("ID: ")).GetAwaiter().GetResult() ? "Da xoa." : "Khong tim thay.");
                else if (c == "4") PrintStudents(_students.GetAllAsync().GetAwaiter().GetResult());
                else if (c == "5") PrintStudents(_students.SearchByNameAsync(Read("Ten: ")).GetAwaiter().GetResult());
                else if (c == "6") { var key = Read("Dien thoai/email: "); var x = _students.GetByPhoneAsync(key).GetAwaiter().GetResult() ?? _students.GetByEmailAsync(key).GetAwaiter().GetResult(); if (x != null) Console.WriteLine($"{x.Id} | {x.FullName} | {x.Phone} | {x.Email}"); }
                Pause();
            } catch (Exception ex) { Error(ex); }
        }
    }

    private void Enrollments()
    {
        while (true)
        {
            Console.Clear(); Console.WriteLine("--- DANG KY --- 1 Dang ky 2 Thanh toan 3 Theo hoc vien 4 Theo lop 5 Cong no 6 Huy 0 Quay lai"); var c = Read("Chon: "); if (c == "0") return;
            try
            {
                if (c == "1") { var s = StudentById(); var cl = FindClass(); if (s != null && cl != null) { var course = _courses.GetByIdAsync(cl.CourseId).GetAwaiter().GetResult() ?? throw new InvalidOperationException("Course khong ton tai"); _enrollments.AddAsync(new Enrollment { StudentId = s.Id, ClassId = cl.Id, TotalFee = course.Fee }).GetAwaiter().GetResult(); } }
                else if (c == "2") Console.WriteLine(_enrollments.RecordPaymentAsync(Read("Enrollment ID: "), Decimal("So tien: ")).GetAwaiter().GetResult() ? "Da ghi nhan." : "Khong tim thay.");
                else if (c == "3") PrintEnrollments(_enrollments.GetByStudentIdAsync(Read("Student ID: ")).GetAwaiter().GetResult());
                else if (c == "4") PrintEnrollments(_enrollments.GetByClassIdAsync(Read("Class ID: ")).GetAwaiter().GetResult());
                else if (c == "5") foreach (var x in _reports.StudentsWithDebtAsync().GetAwaiter().GetResult()) Console.WriteLine($"{x.StudentName} | {x.ClassName} | Con no: {x.Remaining:N0}");
                else if (c == "6") Console.WriteLine(_enrollments.CancelAsync(Read("Enrollment ID: ")).GetAwaiter().GetResult() ? "Da huy." : "Khong tim thay.");
                Pause();
            } catch (Exception ex) { Error(ex); }
        }
    }

    private void Care()
    {
        while (true)
        {
            Console.Clear(); Console.WriteLine("--- CHAM SOC --- 1 Ghi lich su 2 Theo hoc vien 3 Hen hom nay 4 Hen qua han 0 Quay lai"); var c = Read("Chon: "); if (c == "0") return;
            try { if (c == "1") { var s = StudentById(); if (s != null) _care.AddAsync(new CareRecord { StudentId = s.Id, ContactChannel = (ContactChannel)Int("0 Phone, 1 Email, 2 InPerson, 3 SMS: "), Content = Read("Noi dung: "), Result = Read("Ket qua: "), NextAppointment = OptionalDate("Hen tiep theo: ") }).GetAwaiter().GetResult(); } else if (c == "2") foreach (var x in _care.GetByStudentIdAsync(Read("Student ID: ")).GetAwaiter().GetResult()) Console.WriteLine($"{x.CareDate:g} | {x.Content} | {x.Result}"); else if (c == "3") foreach (var x in _reports.TodaysAppointmentsAsync().GetAwaiter().GetResult()) Console.WriteLine($"{x.StudentName} | {x.NextAppointment:g} | {x.Content}"); else if (c == "4") foreach (var x in _care.GetOverdueAppointmentsAsync().GetAwaiter().GetResult()) Console.WriteLine($"{x.StudentId} | {x.NextAppointment:g} | {x.Content}"); Pause(); } catch (Exception ex) { Error(ex); }
        }
    }

    private void Reports()
    {
        Console.Clear(); Console.WriteLine("--- BAO CAO LINQ ---"); foreach (var x in _reports.StudentCountByCourseAsync().GetAwaiter().GetResult()) Console.WriteLine($"Khoa hoc: {x.Key} = {x.Value} hoc vien"); Console.WriteLine($"Tong doanh thu: {_reports.TotalRevenueAsync().GetAwaiter().GetResult():N0}"); var rate = _reports.PaymentCompletionRateAsync().GetAwaiter().GetResult(); Console.WriteLine($"Thanh toan du: {rate.FullyPaid}/{rate.TotalEnrollments} ({rate.CompletionPercentage}%)"); var top = _reports.CourseWithMostStudentsAsync().GetAwaiter().GetResult(); if (top.HasValue) Console.WriteLine($"Dong hoc vien nhat: {top.Value.CourseName} ({top.Value.StudentCount})"); foreach (var x in _reports.RevenueByMonthAsync().GetAwaiter().GetResult()) Console.WriteLine($"{x.Key}: {x.Value:N0}"); Pause();
    }

    private void Csv() { try { if (Read("1 Xuat, 2 Nhap: ") == "1") _csv.ExportToCsvAsync(_students.GetAllAsync().GetAwaiter().GetResult().ToList(), Read("File CSV: ")).GetAwaiter().GetResult(); else foreach (var s in _csv.ImportFromCsvAsync(Read("File CSV: ")).GetAwaiter().GetResult()) _students.AddAsync(s).GetAwaiter().GetResult(); Console.WriteLine("Hoan tat."); } catch (Exception ex) { Error(ex); } Pause(); }
    private void Backups() { try { var c = Read("1 Tao, 2 Xem, 3 Phuc hoi, 4 Xoa: "); if (c == "1") _backups.CreateBackup(); else if (c == "2") foreach (var b in _backups.GetAvailableBackups()) Console.WriteLine($"{b.Name} | {b.FileCount} file | {b.Path}"); else if (c == "3") _backups.RestoreBackup(Read("Duong dan: ")); else if (c == "4") _backups.DeleteBackup(Read("Duong dan: ")); } catch (Exception ex) { Error(ex); } Pause(); }

    private Course? FindCourse() => _courses.GetByIdAsync(Read("Course ID: ")).GetAwaiter().GetResult(); private Class? FindClass() => _classes.GetByIdAsync(Read("Class ID: ")).GetAwaiter().GetResult(); private Student? StudentById() => _students.GetByIdAsync(Read("Student ID: ")).GetAwaiter().GetResult();
    private static string Read(string p) { Console.Write(p); return Console.ReadLine()?.Trim() ?? ""; } private static string ReadOr(string p, string old) { var x = Read($"{p} [{old}]: "); return string.IsNullOrWhiteSpace(x) ? old : x; }
    private static decimal Decimal(string p) => decimal.Parse(Read(p)); private static decimal DecimalOr(string p, decimal old) { var x = Read($"{p} [{old}]: "); return string.IsNullOrWhiteSpace(x) ? old : decimal.Parse(x); } private static int Int(string p) => int.Parse(Read(p)); private static int IntOr(string p, int old) { var x = Read($"{p} [{old}]: "); return string.IsNullOrWhiteSpace(x) ? old : int.Parse(x); } private static DateTime Date(string p) => DateTime.Parse(Read(p)); private static DateTime? OptionalDate(string p) { var x = Read(p); return string.IsNullOrWhiteSpace(x) ? null : DateTime.Parse(x); }
    private static void Pause() { Console.WriteLine("Nhan phim bat ky..."); Console.ReadKey(); } private static void Error(Exception ex) => Console.WriteLine($"Loi: {ex.Message}"); private static void Print(Course x) => Console.WriteLine($"{x.Id} | {x.Name} | {x.Fee:N0} | {x.Duration}h | {x.Status}");
    private static void PrintClasses(IEnumerable<Class> xs) { foreach (var x in xs) Console.WriteLine($"{x.Id} | {x.Name} | {x.StartDate:yyyy-MM-dd} -> {x.EndDate:yyyy-MM-dd} | {x.Status}"); } private static void PrintStudents(IEnumerable<Student> xs) { foreach (var x in xs) Console.WriteLine($"{x.Id} | {x.FullName} | {x.Phone} | {x.Email}"); } private static void PrintEnrollments(IEnumerable<Enrollment> xs) { foreach (var x in xs) Console.WriteLine($"{x.Id} | {x.StudentId} | {x.ClassId} | {x.PaidAmount:N0}/{x.TotalFee:N0} | {x.PaymentStatus} | Active={x.IsActive}"); }
}
