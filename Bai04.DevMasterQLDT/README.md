# DevMaster Training Management System

## Tổng Quan

Hệ thống quản lý đào tạo **DevMaster** là một ứng dụng toàn diện được xây dựng theo kiến trúc nhiều tầng (Clean Architecture) cho phép quản lý:

- **Khóa học** (Course Management)
- **Lớp học** (Class Management)  
- **Học viên** (Student Management)
- **Đăng ký khóa học** (Enrollment Management)
- **Chăm sóc học viên** (Student Care)
- **Báo cáo LINQ** (Analytics & Reports)
- **Xuất/Nhập CSV** (CSV Import/Export)
- **Sao lưu dữ liệu** (Backup/Restore)
- **Ghi log lỗi** (Error Logging)

## Kiến Trúc Dự Án

Dự án được tổ chức theo mô hình **Clean Architecture** với 5 projects chính:

```
DevmasterTrainingManagement.sln
│
├── DevmasterTrainingManagement.Domain
│   ├── Entities/
│   │   ├── Course.cs
│   │   ├── Class.cs
│   │   ├── Student.cs
│   │   ├── Enrollment.cs
│   │   └── CareRecord.cs
│   └── Enums/
│       ├── CourseStatus.cs
│       ├── ClassStatus.cs
│       ├── PaymentStatus.cs
│       └── ContactChannel.cs
│
├── DevmasterTrainingManagement.Application
│   ├── Interfaces/
│   │   ├── IRepository.cs
│   │   ├── ICourseService.cs
│   │   ├── IClassService.cs
│   │   ├── IStudentService.cs
│   │   ├── IEnrollmentService.cs
│   │   └── ICareService.cs
│   ├── Services/
│   │   ├── CourseService.cs
│   │   ├── ClassService.cs
│   │   ├── StudentService.cs
│   │   ├── EnrollmentService.cs
│   │   └── CareService.cs
│   └── Reports/
│       └── ReportService.cs
│
├── DevmasterTrainingManagement.Infrastructure
│   ├── Repositories/
│   │   └── JsonRepository.cs
│   ├── Persistence/
│   │   └── BackupService.cs
│   ├── Csv/
│   │   └── StudentCsvService.cs
│   └── Logging/
│       └── FileLogger.cs
│
├── DevmasterTrainingManagement.ConsoleUI
│   ├── Menus/
│   │   └── MainMenu.cs
│   └── Program.cs
│
└── DevmasterTrainingManagement.Tests
    └── UnitTest1.cs (Comprehensive Unit Tests)
```

## Các Tính Năng Chính

### 1. Quản Lý Khóa Học

- **Thêm khóa học** mới với thông tin: tên, học phí, thời lượng, mô tả
- **Sửa/Xóa** khóa học tồn tại
- **Tìm kiếm** khóa học theo từ khóa
- **Sắp xếp** theo tên, học phí, thời lượng, trạng thái

**Trạng thái khóa học:**
- Draft: Nháp
- Open: Mở nhận đăng ký
- Closed: Đã kết thúc

### 2. Quản Lý Lớp Học

- **Tạo lớp** liên kết với khóa học
- **Xem lớp sắp khai giảng** (startup date >= today)
- **Xem lớp đang học** (between start date và end date)
- **Kiểm tra sĩ số lớp** (số lượng đơn đăng ký vs max students)
- **Lớp đầy** - không cho phép đăng ký thêm

**Trạng thái lớp:**
- Planning: Đang lên kế hoạch
- Scheduled: Đã lên lịch
- InProgress: Đang diễn ra
- Completed: Đã kết thúc
- Cancelled: Đã hủy

### 3. Quản Lý Học Viên

- **Thêm/Sửa/Xóa** học viên
- **Kiểm tra trùng lặp** số điện thoại và email
- **Tìm kiếm** theo tên, số điện thoại, email
- **Thông tin lưu trữ:** CMND, địa chỉ, ngày đăng ký

### 4. Đăng Ký Khóa Học

- **Đăng ký học viên** vào lớp học
- **Kiểm tra điều kiện:**
  - Học viên tồn tại
  - Lớp có còn chỗ
  - Học viên chưa đăng ký lớp này
- **Ghi nhận thanh toán** với cập nhật trạng thái
- **Tính toán công nợ** (số tiền còn lại)

**Trạng thái thanh toán:**
- Pending: Chưa thanh toán
- PartiallyPaid: Thanh toán một phần
- FullyPaid: Thanh toán đủ
- Overdue: Quá hạn

### 5. Chăm Sóc Học Viên

- **Ghi lịch sử chăm sóc** (liên hệ qua điện thoại, email, gặp trực tiếp)
- **Lịch hẹn hôm nay** - xem các cuộc gọi/gặp hôm nay
- **Lịch hẹn quá hạn** - những lịch hẹn chưa thực hiện
- **Xem lịch sử** theo học viên

### 6. Báo Cáo LINQ (12 Báo Cáo)

1. **Số học viên theo khóa học** - đếm số học viên đã đăng ký mỗi khóa
2. **Số học viên theo lớp** - đếm số học viên mỗi lớp
3. **Lớp sắp khai giảng** - danh sách lớp trong vòng 7 ngày tới
4. **Học viên còn nợ học phí** - những người chưa thanh toán đủ
5. **Tổng doanh thu** - tổng tiền đã thu được
6. **Doanh thu theo tháng** - thống kê doanh thu từng tháng
7. **Khóa học có nhiều học viên nhất** - top course
8. **Lịch hẹn hôm nay** - những cuộc chăm sóc hôm nay
9. **Học viên bị bỏ quên** - lâu ngày không liên hệ
10. **Tỷ lệ thanh toán** - % học viên thanh toán đủ
11. **Lớp theo trạng thái** - phân loại lớp theo tình trạng
12. **Tỷ lệ sĩ số lớp** - công suất sử dụng mỗi lớp

### 7. Import/Export CSV

- **Xuất học viên** sang file CSV
- **Nhập học viên** từ file CSV với:
  - Kiểm tra định dạng ngày tháng
  - Kiểm tra số điện thoại trùng
  - Ghi nhật ký lỗi nếu dữ liệu sai

### 8. Sao Lưu & Phục Hồi

- **Tạo backup** - sao chép toàn bộ dữ liệu JSON
- **Liệt kê backup** - xem các bản sao lưu đã tạo
- **Phục hồi** - khôi phục dữ liệu từ một backup
- **Xóa backup** - xóa bản sao lưu cũ

### 9. Ghi Log Lỗi

- **Tệp log** lưu tại `Logs/app.log`
- **Ghi nhật ký** cho mỗi thao tác quan trọng
- **Mức độ:** Info, Warning, Error, Debug
- **Format:** `[YYYY-MM-DD HH:MM:SS] [LEVEL] Message`

## Dữ Liệu và Lưu Trữ

### JSON Storage

Dữ liệu được lưu trữ thành file JSON riêng biệt:

```
Data/
├── courses.json      # Dữ liệu khóa học
├── classes.json      # Dữ liệu lớp học
├── students.json     # Dữ liệu học viên
├── enrollments.json  # Dữ liệu đăng ký
└── carerecords.json  # Dữ liệu chăm sóc
```

### Backup Structure

```
Backup/
├── backup_2026-08-15_1630/
│   ├── courses.json
│   ├── classes.json
│   ├── students.json
│   ├── enrollments.json
│   └── carerecords.json
└── backup_2026-08-15_1700/
    └── ...
```

## Sơ Đồ Quan Hệ Dữ Liệu

```
Course
   │
   └──────< Class (1 khóa : nhiều lớp)
              │
              └──────< Enrollment >────── Student
                          │              (1 học viên: nhiều đăng ký)
                          │
                          │
              Payment Status
                      │
                 (Pending / PartiallyPaid / FullyPaid)
                
Student
   │
   └──────< CareRecord (1 học viên: nhiều lần chăm sóc)
```

## Hướng Dẫn Sử Dụng

### Chạy Ứng Dụng

```bash
cd DevmasterTrainingManagement.ConsoleUI
dotnet run
```

### Cấu Trúc Menu

```
┌────────────────────────────────────┐
│  DEVMASTER TRAINING MANAGEMENT     │
└────────────────────────────────────┘
1. Course Management         → Quản lý khóa học
2. Class Management         → Quản lý lớp học
3. Student Management       → Quản lý học viên
4. Enrollment Management    → Quản lý đăng ký
5. Student Care             → Quản lý chăm sóc
6. Reports & Analytics      → Báo cáo
7. CSV Import/Export        → Nhập xuất CSV
8. Backup & Restore         → Sao lưu phục hồi
9. Exit                     → Thoát chương trình
```

## Kiểm Thử (Unit Tests)

Dự án bao gồm các unit tests toàn diện:

```bash
cd DevmasterTrainingManagement.Tests
dotnet test
```

### Các Test Bao Gồm:

- ✅ **CourseServiceTests**
  - Add, Update, Delete, Search, Sort Courses
  
- ✅ **StudentServiceTests**
  - Add, Update, Delete, Search Students
  - Check duplicate phone/email
  
- ✅ **EnrollmentServiceTests**
  - Create enrollment with correct payment status
  - Record payment
  - Calculate remaining balance
  
- ✅ **BackupServiceTests**
  - Create and restore backups

## Công Nghệ Sử Dụng

- **Language:** C# 12 / .NET 10
- **Architecture:** Clean Architecture
- **Testing:** xUnit
- **Data Format:** JSON
- **CSV Processing:** CsvHelper v33.1.0
- **Logging:** Custom FileLogger
- **LINQ:** Extensive LINQ queries for reporting

## Các Enum được sử dụng

```csharp
// CourseStatus
Draft = 0, Open = 1, Closed = 2

// ClassStatus
Planning = 0, Scheduled = 1, InProgress = 2, Completed = 3, Cancelled = 4

// PaymentStatus
Pending = 0, PartiallyPaid = 1, FullyPaid = 2, Overdue = 3

// ContactChannel
Phone = 0, Email = 1, InPerson = 2, SMS = 3
```

## Đặc Điểm Nổi Bật

### ✨ Kiến Trúc Sạch

- **Separation of Concerns** - mỗi layer có trách nhiệm riêng
- **Dependency Injection** - dễ test và maintain
- **Generic Repository Pattern** - `JsonRepository<T>`
- **Service Layer** - logic nghiệp vụ tập trung

### ✨ Xử Lý Lỗi

- **Try-Catch** - bắt lỗi ở tầng application
- **File Logging** - ghi tất cả lỗi vào log
- **Validation** - kiểm tra dữ liệu input
- **Graceful Error Messages** - thông báo rõ ràng

### ✨ Tính Năng Bảo Mật

- **Kiểm tra duplicate** - số điện thoại, email
- **Kiểm tra điều kiện** - lớp đầy, học viên trùng
- **Backup tự động** - sao lưu trước khi phục hồi

### ✨ Báo Cáo LINQ

- **12 báo cáo** sử dụng LINQ queries
- **GroupBy, OrderBy, Join** - các toán tử LINQ
- **Tính toán thống kê** - sum, count, average

## File Cấu Hình

### Data Directory

Ứng dụng tự động tạo các thư mục:

```
ApplicationDirectory/
├── Data/              # JSON files
├── Logs/              # Log files
├── Backup/            # Backup directories
└── Export/            # CSV exports (optional)
```

## Hướng Phát Triển Tiếp Theo

1. 🔄 **Database Integration** - thay JSON bằng SQL Server
2. 📱 **API RESTful** - tạo Web API
3. 🎨 **WPF/WinForms UI** - giao diện desktop đẹp
4. 📊 **Dashboard** - bảng điều khiển trực quan
5. 🔐 **Authentication** - xác thực người dùng
6. 📧 **Email Notifications** - thông báo qua email
7. 📱 **Mobile App** - ứng dụng di động

## Yêu Cầu Hệ Thống

- **.NET Runtime:** .NET 10 trở lên
- **OS:** Windows, Linux, macOS
- **Disk Space:** Tối thiểu 100MB
- **RAM:** Tối thiểu 256MB

## Cài Đặt

```bash
# Clone repository
git clone <repository-url>

# Navigate to solution
cd DevmasterTrainingManagement.sln

# Restore dependencies
dotnet restore

# Build solution
dotnet build

# Run tests
dotnet test

# Run application
cd DevmasterTrainingManagement.ConsoleUI
dotnet run
```

## Giấy Phép

MIT License - Miễn phí sử dụng

## Tác Giả

Developed as a comprehensive training management system for DevMaster

---

**Cập nhật lần cuối:** 2026-08-15  
**Phiên bản:** 1.0.0  
**Trạng thái:** ✅ Hoàn thành chức năng cơ bản
