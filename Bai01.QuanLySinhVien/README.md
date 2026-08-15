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
Bai01.QuanLySinhVien/ ├── Models/ │ └── Student.cs ├── Enums/ │ ├── Gender.cs │ └── StudentStatus.cs ├── Helpers/ │ └── InputHelper.cs ├── Validators/ │ └── StudentValidator.cs ├── Services/ │ └── StudentService.cs ├── Views/ │ └── StudentConsoleView.cs ├── Managers/ │ └── MenuManager.cs ├── Screenshots/ └── Program.cs


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