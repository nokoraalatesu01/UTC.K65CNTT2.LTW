# Bai01 - Quản Lý Sinh Viên

## Mục tiêu
Xây dựng chương trình quản lý sinh viên bằng C# .NET 8 Console App, vận dụng OOP, Collection, LINQ, Validation.

## Công nghệ
- C# 12, .NET 8
- Console App
- List<T>, LINQ
- Regular Expression
- Nullable Reference Types

## Cấu trúc project
```
Bai01.QuanLySinhVien/
├── Models/
│   └── Student.cs
├── Enums/
│   ├── Gender.cs
│   └── StudentStatus.cs
├── Helpers/
│   └── InputHelper.cs
├── Validators/
│   ├── StudentValidator.cs
│   └── InputHelper.cs
├── Services/
│   └── StudentService.cs
├── Views/
│   └── StudentConsoleView.cs
├── Managers/
│   └── MenuManager.cs
├── Screenshots/
└── Program.cs
```

## Chức năng đã hoàn thành
1. Thêm sinh viên
2. Hiển thị danh sách
3. Tìm sinh viên theo mã
4. Tìm gần đúng theo họ tên
5. Cập nhật sinh viên
6. Xóa sinh viên
7. Sắp xếp theo họ tên
8. Sắp xếp theo điểm trung bình
9. Sinh viên có GPA từ 8 trở lên
10. Sinh viên có điểm cao nhất
11. Tính GPA trung bình
12. Thống kê theo ngành
13. Thống kê theo trạng thái

## Chức năng chưa hoàn thành
- Không có

## Dữ liệu mẫu
Chương trình có 3 sinh viên mẫu trong `StudentService.SeedData()`.

## Hướng dẫn chạy
```bash
dotnet run --project Bai01.QuanLySinhVien/Bai01.QuanLySinhVien.csproj
```

## Đánh giá sản phẩm

# Mã nguồn rõ ràng.
- Đặt tên biến, phương thức theo chuẩn C# rõ ràng.
- Mỗi file có một class hoặc enum duy nhất.
- Không viết toàn bộ logic trong `Program.cs`.

# Chức năng đúng nghiệp vụ.
- 13 chức năng đều có mặt trong `MenuManager`.
- CRUD hoạt động đúng luồng: kiểm tra tồn tại trước khi cập nhật/xóa.
- Validation ngày sinh, email, SĐT, GPA theo yêu cầu.

# Có khả năng xử lý lỗi.
- `StudentValidator.IsValid` kiểm tra ràng buộc nghiệp vụ.
- `InputHelper` có vòng lặp nhập lại đến khi hợp lệ.
- Xử lý danh sách rỗng trước khi hiển thị, tính trung bình, lấy top student.

# Có khả năng mở rộng chương trình.
- Cấu trúc phân lớp rõ ràng: View → Manager → Service → Validator.
- Dễ thêm chức năng mới bằng cách bổ sung method trong `StudentService` và case trong `MenuManager`.
- Dễ thay đổi nơi lưu trữ: chỉ cần sửa `StudentService` mà không đụng đến View hay Menu.

# Có khả năng đọc và sửa mã nguồn.
- Code theo chuẩn C# 12, dùng LINQ thay vì vòng lặp thủ công phức tạp.
- Các method ngắn, có chức năng rõ ràng, dễ đọc.
- Không hard-code nhiều, dữ liệu mẫu tách riêng trong `SeedData()`.

# Có thể tham gia phát triển module nhỏ dưới sự hướng dẫn của người phụ trách kỹ thuật.
- Hiểu rõ luồng dữ liệu từ nhập liệu → validation → xử lý nghiệp vụ → hiển thị.
- Có thể phát triển thêm module `LuuTru/StudentRepository`, `BaoCao/ReportService`, `TimKiemNangCao/AdvancedSearch`.
- Có thể phát triển giao diện Console nâng cao hoặc chuyển sang WinForms/WPF dựa trên `StudentConsoleView`.

# Đủ nền tảng để chuyển sang học ASP.NET Core MVC và ASP.NET Core Web API.
- Đã nắm Separation of Concerns: Model, Service, Validator, View.
- Đã dùng Dependency Injection thủ công (`StudentService` nhận `StudentValidator` qua constructor).
- Đã nắm CRUD, Validation, LINQ — là nền tảng để viết Controller, Service, Repository trong ASP.NET Core.
- Dễ chuyển `StudentService` thành `IStudentService` interface và `StudentRepository` pattern.
- Dễ thay thế Console View bằng Controller + View trong ASP.NET Core MVC.
