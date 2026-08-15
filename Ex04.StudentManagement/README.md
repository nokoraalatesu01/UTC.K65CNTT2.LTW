# BÀI 4. QUẢN LÝ SINH VIÊN BẰNG OOP

> Thực hành tổng hợp với C# .NET 8 Console App

---

## 1. Giới thiệu

Trong bài thực hành này, sinh viên xây dựng chương trình **Quản lý sinh viên** bằng **C# .NET 8 Console App**.

Bài tập tập trung vào việc vận dụng kiến thức **lập trình hướng đối tượng (OOP)** kết hợp với Collection, Validation và LINQ cơ bản để xây dựng một chương trình quản lý dữ liệu hoàn chỉnh.

Chương trình quản lý danh sách sinh viên trong bộ nhớ bằng `List<Student>` và cung cấp các chức năng:

- Thêm sinh viên.
- Hiển thị danh sách sinh viên.
- Tìm sinh viên theo mã.
- Tìm gần đúng theo họ tên.
- Cập nhật thông tin sinh viên.
- Xóa sinh viên.
- Sắp xếp theo họ tên.
- Sắp xếp theo điểm trung bình.
- Hiển thị sinh viên có điểm trung bình từ 8 trở lên.
- Hiển thị sinh viên có điểm cao nhất.
- Tính điểm trung bình của toàn bộ sinh viên.
- Thống kê sinh viên theo ngành.
- Thống kê sinh viên theo trạng thái học tập.

---

# 2. Mục tiêu

## 2.1. Kiến thức

Sau khi hoàn thành bài thực hành, sinh viên có thể:

- Hiểu Class và Object.
- Sử dụng Constructor.
- Sử dụng Property.
- Hiểu và áp dụng Encapsulation.
- Sử dụng `private set`.
- Sử dụng Static Member.
- Sử dụng Nullable Reference Types.
- Sử dụng `List<T>`.
- Sử dụng `enum`.
- Sử dụng Regular Expression.
- Sử dụng LINQ cơ bản.
- Biết phân chia trách nhiệm giữa các lớp trong chương trình.

## 2.2. Kỹ năng

- Phân tích yêu cầu bài toán.
- Thiết kế class.
- Xây dựng chương trình theo hướng OOP.
- Nhập và kiểm tra dữ liệu từ Console.
- Xử lý dữ liệu bằng `List<T>`.
- Thực hiện CRUD.
- Tìm kiếm dữ liệu.
- Sắp xếp dữ liệu.
- Lọc dữ liệu.
- Thống kê dữ liệu.
- Tổ chức source code theo thư mục.
- Sử dụng Git để quản lý source code.

---

# 3. Công nghệ sử dụng

| Công nghệ | Phiên bản |
|---|---|
| C# | 12 |
| .NET | 8 |
| Application | Console App |
| IDE | Visual Studio 2022 |
| Collection | `List<T>` |
| LINQ | Cơ bản |
| Git | Khuyến khích |

---

# 4. Nội dung kiến thức

Bài thực hành luyện tập:

- Class và Object.
- Constructor.
- Property.
- Encapsulation.
- Static Member.
- Nullable Reference Types.
- List<T>.
- Enum.
- Regular Expression.
- LINQ.
- CRUD.
- Validation.
- Tìm kiếm.
- Sắp xếp.
- Thống kê.

---

# 5. Thông tin sinh viên

Mỗi sinh viên gồm các thông tin:

| STT | Thông tin | Kiểu dữ liệu đề xuất |
|---:|---|---|
| 1 | Mã sinh viên | `string` |
| 2 | Họ tên | `string` |
| 3 | Ngày sinh | `DateTime` |
| 4 | Giới tính | `Gender` |
| 5 | Email | `string` |
| 6 | Số điện thoại | `string` |
| 7 | Ngành học | `string` |
| 8 | Điểm trung bình | `double` |
| 9 | Trạng thái học tập | `StudentStatus` |

---

# 6. Chức năng chương trình

| STT | Chức năng |
|---:|---|
| 1 | Thêm sinh viên |
| 2 | Hiển thị danh sách |
| 3 | Tìm sinh viên theo mã |
| 4 | Tìm gần đúng theo họ tên |
| 5 | Cập nhật sinh viên |
| 6 | Xóa sinh viên |
| 7 | Sắp xếp theo họ tên |
| 8 | Sắp xếp theo điểm trung bình |
| 9 | Hiển thị sinh viên có điểm từ 8 trở lên |
| 10 | Hiển thị sinh viên có điểm cao nhất |
| 11 | Tính điểm trung bình toàn bộ sinh viên |
| 12 | Thống kê sinh viên theo ngành |
| 13 | Thống kê sinh viên theo trạng thái |

---

# 7. Quy tắc nghiệp vụ

## 7.1. Mã sinh viên

- Không được để trống.
- Không được trùng.
- Phải kiểm tra trước khi thêm.

Ví dụ:

```text
SV001
SV002
SV003
SV004
```

Nếu mã đã tồn tại:

```text
Mã sinh viên đã tồn tại. Vui lòng nhập mã khác!
```

## 7.2. Họ tên

- Không được để trống.
- Không được chỉ chứa khoảng trắng.

## 7.3. Ngày sinh

- Phải nhập đúng định dạng.
- Phải là ngày hợp lệ.
- Không được lớn hơn ngày hiện tại.

## 7.4. Email

Email phải đúng định dạng.

Ví dụ hợp lệ:

```text
student@gmail.com
student01@example.com
nguyenvana@devmaster.edu.vn
```

Regular Expression đề xuất:

```regex
^[^@\s]+@[^@\s]+\.[^@\s]{2,4}$
```

## 7.5. Số điện thoại

- Chỉ chứa chữ số.
- Có từ 9 đến 11 chữ số.

Regular Expression:

```regex
^\d{9,11}$
```

## 7.6. Điểm trung bình

Điểm trung bình phải thỏa:

```text
0 <= GPA <= 10
```

## 7.7. Cập nhật

Chỉ được cập nhật khi sinh viên tồn tại.

## 7.8. Xóa

Chỉ được xóa khi sinh viên tồn tại.

Khuyến khích yêu cầu xác nhận:

```text
Bạn có chắc chắn muốn xóa sinh viên này? (Y/N):
```

---

# 8. Cấu trúc project

Đề xuất tổ chức source code:

```text
Ex04.StudentManagement/
│
├── Models/
│   └── Student.cs
│
├── Enums/
│   ├── Gender.cs
│   └── StudentStatus.cs
│
├── Helpers/
│   └── InputHelper.cs
│
├── Validators/
│   └── StudentValidator.cs
│
├── Services/
│   └── StudentService.cs
│
├── Views/
│   └── StudentConsoleView.cs
│
├── Managers/
│   └── MenuManager.cs
│
└── Program.cs
```

---

# 9. Mô tả trách nhiệm các lớp

## 9.1. Student

Đại diện cho một sinh viên.

Các thuộc tính:

```csharp
StudentId
FullName
DateOfBirth
Gender
Email
Phone
Major
GPA
Status
```

Nên áp dụng Encapsulation:

```csharp
public string FullName { get; private set; }
public double GPA { get; private set; }
```

---

## 9.2. Gender

Enum giới tính:

```csharp
public enum Gender
{
    Male = 1,
    Female = 2,
    Other = 3
}
```

---

## 9.3. StudentStatus

Enum trạng thái học tập:

```csharp
public enum StudentStatus
{
    Studying = 1,
    Reserved = 2,
    Graduated = 3,
    DroppedOut = 4
}
```

Ý nghĩa:

| Giá trị | Ý nghĩa |
|---|---|
| Studying | Đang học |
| Reserved | Bảo lưu |
| Graduated | Đã tốt nghiệp |
| DroppedOut | Thôi học |

---

# 10. InputHelper

`InputHelper` chịu trách nhiệm nhập dữ liệu và kiểm tra dữ liệu cơ bản.

Các phương thức đề xuất:

```csharp
ReadNonEmptyString()
ReadPhoneNumber()
ReadEmail()
ReadInt()
ReadDouble()
ReadDate()
ReadYesNo()
```

Ví dụ:

```csharp
double gpa = InputHelper.ReadDouble(
    "Điểm trung bình: ",
    0,
    10
);
```

Class `InputHelper` có thể sử dụng các phương thức `static` để gọi trực tiếp:

```csharp
string name = InputHelper.ReadNonEmptyString("Họ tên: ");
```

---

# 11. StudentValidator

`StudentValidator` chịu trách nhiệm kiểm tra dữ liệu nghiệp vụ của sinh viên.

Các nội dung:

- Mã sinh viên.
- Họ tên.
- Ngày sinh.
- Email.
- Số điện thoại.
- GPA.
- Các dữ liệu bắt buộc khác.

Có thể thiết kế:

```csharp
public bool IsValid(Student student, out string message)
{
    // Kiểm tra dữ liệu
}
```

Mục tiêu là tách phần **validation nghiệp vụ** khỏi phần nhập dữ liệu và xử lý nghiệp vụ.

---

# 12. StudentService

`StudentService` chịu trách nhiệm xử lý nghiệp vụ quản lý sinh viên.

Dữ liệu được lưu trong:

```csharp
private readonly List<Student> _students;
```

Các phương thức đề xuất:

```csharp
Add()
GetAll()
GetById()
SearchByName()
Update()
Delete()
SortByName()
SortByGPA()
GetStudentsGPAFrom8()
GetTopStudent()
GetAverageGPA()
StatisticsByMajor()
StatisticsByStatus()
```

---

# 13. StudentConsoleView

`StudentConsoleView` chịu trách nhiệm giao tiếp với người dùng qua Console.

Nhiệm vụ:

- Hiển thị thông tin.
- Hiển thị danh sách.
- Nhập thông tin sinh viên.
- Hiển thị kết quả tìm kiếm.
- Hiển thị kết quả thống kê.
- Hiển thị thông báo.

Ví dụ:

```text
================ DANH SÁCH SINH VIÊN ================

Mã      Họ tên              Ngành           GPA
SV001   Nguyễn Văn An       CNTT            8.50
SV002   Trần Thị Bình       CNTT            7.80
SV003   Lê Văn Cường        Kinh doanh      9.20

=======================================================
```

---

# 14. MenuManager

`MenuManager` quản lý menu chính.

Menu đề xuất:

```text
========================================================
              QUẢN LÝ SINH VIÊN - C#
========================================================

1.  Thêm sinh viên
2.  Hiển thị danh sách
3.  Tìm sinh viên theo mã
4.  Tìm gần đúng theo họ tên
5.  Cập nhật sinh viên
6.  Xóa sinh viên
7.  Sắp xếp theo họ tên
8.  Sắp xếp theo điểm trung bình
9.  Hiển thị sinh viên có GPA từ 8 trở lên
10. Hiển thị sinh viên có điểm cao nhất
11. Tính điểm trung bình toàn bộ sinh viên
12. Thống kê sinh viên theo ngành
13. Thống kê sinh viên theo trạng thái
0.  Thoát

========================================================
Lựa chọn:
```

---

# 15. Luồng xử lý tổng quát

```text
                    Program
                       |
                       v
                 MenuManager
                       |
                       v
              StudentConsoleView
                       |
                       v
                  InputHelper
                       |
                       v
               StudentValidator
                       |
                       v
                 StudentService
                       |
                       v
                  List<Student>
```

Nguyên tắc:

- `InputHelper`: nhập dữ liệu.
- `Validator`: kiểm tra dữ liệu.
- `Service`: xử lý nghiệp vụ.
- `View`: hiển thị.
- `MenuManager`: điều khiển luồng chương trình.

---

# 16. Chức năng 1 - Thêm sinh viên

Quy trình:

```text
Nhập mã sinh viên
        ↓
Kiểm tra mã có trùng?
        ↓
Nhập họ tên
        ↓
Nhập ngày sinh
        ↓
Nhập giới tính
        ↓
Nhập email
        ↓
Nhập số điện thoại
        ↓
Nhập ngành học
        ↓
Nhập GPA
        ↓
Nhập trạng thái
        ↓
Validate
        ↓
Thêm vào List<Student>
```

Ví dụ:

```text
Mã sinh viên: SV006
Họ tên: Nguyễn Văn Nam
Ngày sinh: 10/05/2005
Giới tính: Nam
Email: nam@gmail.com
Số điện thoại: 0988111222
Ngành học: Công nghệ thông tin
Điểm trung bình: 8.2
Trạng thái: Đang học

=> Thêm sinh viên thành công!
```

---

# 17. Chức năng 2 - Hiển thị danh sách

Hiển thị toàn bộ sinh viên trong `List<Student>`.

Thông tin tối thiểu:

- Mã sinh viên.
- Họ tên.
- Ngày sinh.
- Giới tính.
- Email.
- Số điện thoại.
- Ngành học.
- GPA.
- Trạng thái.

Cần xử lý trường hợp danh sách rỗng:

```text
Danh sách sinh viên đang trống.
```

---

# 18. Chức năng 3 - Tìm sinh viên theo mã

Sử dụng `FirstOrDefault()`:

```csharp
var student = _students
    .FirstOrDefault(s => s.StudentId == studentId);
```

Nếu không tìm thấy:

```text
Không tìm thấy sinh viên.
```

Khi dùng Nullable Reference Types:

```csharp
Student? student = _studentService.GetById(studentId);
```

---

# 19. Chức năng 4 - Tìm gần đúng theo họ tên

Cho phép nhập một phần họ tên.

Ví dụ nhập:

```text
Nguyễn
```

Có thể tìm được:

```text
Nguyễn Văn An
Nguyễn Thị Hoa
Nguyễn Văn Minh
```

Nên tìm không phân biệt chữ hoa/chữ thường:

```csharp
var result = _students
    .Where(s => s.FullName.Contains(
        keyword,
        StringComparison.OrdinalIgnoreCase))
    .ToList();
```

---

# 20. Chức năng 5 - Cập nhật sinh viên

Quy trình:

```text
Nhập mã sinh viên
        ↓
Tìm sinh viên
        ↓
Có tồn tại?
    /       \
  Không      Có
   |          |
 Báo lỗi   Nhập dữ liệu mới
              |
              v
           Validate
              |
              v
           Cập nhật
```

Chỉ được cập nhật khi sinh viên tồn tại.

---

# 21. Chức năng 6 - Xóa sinh viên

Quy trình:

```text
Nhập mã sinh viên
        ↓
Tìm sinh viên
        ↓
Có tồn tại?
    /       \
  Không      Có
   |          |
 Báo lỗi    Hiển thị
            thông tin
               |
               v
          Xác nhận Y/N
               |
               v
             Xóa
```

Ví dụ:

```text
Bạn có chắc chắn muốn xóa sinh viên SV002? (Y/N): Y

Xóa sinh viên thành công!
```

---

# 22. Chức năng 7 - Sắp xếp theo họ tên

Sắp xếp tăng dần:

```csharp
var result = _students
    .OrderBy(s => s.FullName)
    .ToList();
```

Sử dụng LINQ:

```text
OrderBy()
```

---

# 23. Chức năng 8 - Sắp xếp theo GPA

Sắp xếp giảm dần:

```csharp
var result = _students
    .OrderByDescending(s => s.GPA)
    .ToList();
```

Sinh viên có GPA cao nhất đứng đầu.

---

# 24. Chức năng 9 - Sinh viên có GPA từ 8 trở lên

Điều kiện:

```text
GPA >= 8
```

Ví dụ:

```csharp
var result = _students
    .Where(s => s.GPA >= 8)
    .ToList();
```

---

# 25. Chức năng 10 - Sinh viên có GPA cao nhất

Ví dụ:

```csharp
var student = _students
    .OrderByDescending(s => s.GPA)
    .FirstOrDefault();
```

Nếu danh sách rỗng phải xử lý trước khi lấy kết quả.

Có thể mở rộng để hiển thị tất cả sinh viên có cùng GPA cao nhất.

---

# 26. Chức năng 11 - Tính GPA trung bình

Công thức:

```text
GPA trung bình = Tổng GPA / Số lượng sinh viên
```

LINQ:

```csharp
double average = _students.Average(s => s.GPA);
```

Cần xử lý trường hợp danh sách rỗng trước khi gọi `Average()`.

---

# 27. Chức năng 12 - Thống kê theo ngành

Sử dụng `GroupBy()`.

Ví dụ:

```csharp
var result = _students
    .GroupBy(s => s.Major)
    .Select(g => new
    {
        Major = g.Key,
        Count = g.Count()
    })
    .ToList();
```

Kết quả:

```text
================ THỐNG KÊ THEO NGÀNH ================

Công nghệ thông tin       : 15 sinh viên
Kinh doanh                : 10 sinh viên
Marketing                 : 8 sinh viên
Điện - Điện tử            : 5 sinh viên

=======================================================
```

---

# 28. Chức năng 13 - Thống kê theo trạng thái

Sử dụng `GroupBy()`:

```csharp
var result = _students
    .GroupBy(s => s.Status)
    .Select(g => new
    {
        Status = g.Key,
        Count = g.Count()
    })
    .ToList();
```

Ví dụ:

```text
================ THỐNG KÊ TRẠNG THÁI ================

Đang học       : 25 sinh viên
Bảo lưu        : 3 sinh viên
Đã tốt nghiệp  : 5 sinh viên
Thôi học       : 2 sinh viên

=======================================================
```

---

# 29. Các LINQ cần luyện tập

| LINQ | Mục đích |
|---|---|
| `Where()` | Lọc dữ liệu |
| `FirstOrDefault()` | Tìm phần tử |
| `OrderBy()` | Sắp xếp tăng |
| `OrderByDescending()` | Sắp xếp giảm |
| `Average()` | Tính trung bình |
| `GroupBy()` | Gom nhóm |
| `Any()` | Kiểm tra tồn tại |
| `ToList()` | Chuyển kết quả thành List |

Ví dụ kết hợp:

```csharp
var result = _students
    .Where(s => s.GPA >= 8)
    .OrderByDescending(s => s.GPA)
    .ToList();
```

---

# 30. Nullable Reference Types

Project nên bật Nullable Reference Types.

Ví dụ:

```csharp
Student? student = _studentService.GetById(studentId);
```

Kiểm tra `null`:

```csharp
if (student is null)
{
    Console.WriteLine("Không tìm thấy sinh viên.");
    return;
}
```

Mục tiêu là hạn chế:

```text
NullReferenceException
```

---

# 31. Static Member

`InputHelper` có thể sử dụng các phương thức `static`:

```csharp
public static string ReadNonEmptyString(string prompt)
{
    // ...
}
```

Sử dụng:

```csharp
string name = InputHelper.ReadNonEmptyString("Họ tên: ");
```

Không cần:

```csharp
new InputHelper();
```

---

# 32. Dữ liệu mẫu

Để thuận tiện kiểm thử:

| Mã | Họ tên | Ngành | GPA | Trạng thái |
|---|---|---|---:|---|
| SV001 | Nguyễn Văn An | CNTT | 8.5 | Đang học |
| SV002 | Trần Thị Bình | CNTT | 7.8 | Đang học |
| SV003 | Lê Văn Cường | Kinh doanh | 9.2 | Đang học |
| SV004 | Phạm Thị Dung | Marketing | 6.9 | Bảo lưu |
| SV005 | Hoàng Văn Minh | CNTT | 8.9 | Đã tốt nghiệp |

---

# 33. Hướng dẫn tạo project

Trong Visual Studio 2022:

```text
Create a new project
        ↓
Console App
        ↓
C#
        ↓
.NET 8
```

Tên project:

```text
Ex04.StudentManagement
```

---

# 34. Tạo thư mục

Tạo:

```text
Models
Enums
Helpers
Validators
Services
Views
Managers
```

Tạo các file:

```text
Models/Student.cs

Enums/Gender.cs
Enums/StudentStatus.cs

Helpers/InputHelper.cs

Validators/StudentValidator.cs

Services/StudentService.cs

Views/StudentConsoleView.cs

Managers/MenuManager.cs

Program.cs
```

---

# 35. Build và chạy

Build:

```text
Ctrl + Shift + B
```

Chạy:

```text
F5
```

hoặc:

```text
Ctrl + F5
```

Có thể chạy bằng CLI:

```bash
dotnet build
dotnet run
```

---

# 36. Kiểm thử chương trình

## Test Case 01 - Thêm sinh viên hợp lệ

Input:

```text
Mã: SV006
Họ tên: Nguyễn Văn Nam
GPA: 8.2
```

Expected:

```text
Thêm sinh viên thành công.
```

## Test Case 02 - Trùng mã

Input:

```text
SV001
```

Expected:

```text
Mã sinh viên đã tồn tại.
```

## Test Case 03 - Họ tên rỗng

Expected:

```text
Dữ liệu không được để trống.
```

## Test Case 04 - GPA nhỏ hơn 0

Input:

```text
-1
```

Expected:

```text
Giá trị không hợp lệ.
```

## Test Case 05 - GPA lớn hơn 10

Input:

```text
11
```

Expected:

```text
Giá trị không hợp lệ.
```

## Test Case 06 - Email sai

Input:

```text
abc
```

Expected:

```text
Email không đúng định dạng.
```

## Test Case 07 - Số điện thoại sai

Input:

```text
123
```

Expected:

```text
Số điện thoại không hợp lệ.
```

## Test Case 08 - Tìm sinh viên không tồn tại

Input:

```text
SV999
```

Expected:

```text
Không tìm thấy sinh viên.
```

## Test Case 09 - Xóa sinh viên

Input:

```text
SV002
Y
```

Expected:

```text
Xóa sinh viên thành công.
```

## Test Case 10 - Tìm GPA cao nhất

Expected:

```text
Hiển thị sinh viên có GPA cao nhất.
```

---

# 37. Yêu cầu Git

Khởi tạo:

```bash
git init
```

Kiểm tra:

```bash
git status
```

Add:

```bash
git add .
```

Commit:

```bash
git commit -m "Complete Ex04 Student Management"
```

Kết nối repository:

```bash
git remote add origin <repository-url>
```

Push:

```bash
git push -u origin main
```

---

# 38. Quy ước Commit

Khuyến khích commit theo chức năng:

```text
Initial project
Create Student model
Create Gender enum
Create StudentStatus enum
Add InputHelper
Add StudentValidator
Implement StudentService
Implement add student
Implement student list
Implement search
Implement update
Implement delete
Implement sorting
Implement statistics
Complete README
```

Không nên dùng commit quá chung chung:

```text
Update
Done
Fix
Test
abc
```

---

# 39. Screenshot cần nộp

Sinh viên nên chụp tối thiểu:

1. Menu chính.
2. Danh sách sinh viên.
3. Thêm sinh viên thành công.
4. Validation dữ liệu sai.
5. Tìm kiếm sinh viên.
6. Cập nhật sinh viên.
7. Xóa sinh viên.
8. Sắp xếp theo GPA.
9. Sinh viên có GPA từ 8 trở lên.
10. Thống kê theo ngành.
11. Thống kê theo trạng thái.

---

# 40. Checklist hoàn thành

## Source Code

- [ ] `Student.cs`
- [ ] `Gender.cs`
- [ ] `StudentStatus.cs`
- [ ] `InputHelper.cs`
- [ ] `StudentValidator.cs`
- [ ] `StudentService.cs`
- [ ] `StudentConsoleView.cs`
- [ ] `MenuManager.cs`
- [ ] `Program.cs`

## Chức năng

- [ ] Thêm sinh viên
- [ ] Hiển thị danh sách
- [ ] Tìm theo mã
- [ ] Tìm gần đúng theo họ tên
- [ ] Cập nhật
- [ ] Xóa
- [ ] Sắp xếp theo họ tên
- [ ] Sắp xếp theo GPA
- [ ] Lọc GPA >= 8
- [ ] Tìm GPA cao nhất
- [ ] Tính GPA trung bình
- [ ] Thống kê theo ngành
- [ ] Thống kê theo trạng thái

## Validation

- [ ] Mã sinh viên không trùng
- [ ] Họ tên không rỗng
- [ ] Ngày sinh hợp lệ
- [ ] Email hợp lệ
- [ ] Số điện thoại hợp lệ
- [ ] GPA từ 0 đến 10
- [ ] Kiểm tra tồn tại trước Update
- [ ] Kiểm tra tồn tại trước Delete

## OOP

- [ ] Class
- [ ] Object
- [ ] Constructor
- [ ] Property
- [ ] Encapsulation
- [ ] Static Member
- [ ] Nullable Reference Types
- [ ] Enum
- [ ] List<T>

## LINQ

- [ ] `Where()`
- [ ] `FirstOrDefault()`
- [ ] `OrderBy()`
- [ ] `OrderByDescending()`
- [ ] `Average()`
- [ ] `GroupBy()`
- [ ] `Any()`
- [ ] `ToList()`

## Git

- [ ] Git repository
- [ ] Commit source code
- [ ] Push lên GitHub/GitLab
- [ ] README.md
- [ ] Screenshot

---

# 41. Yêu cầu nâng cao

## Level 1 - Tìm kiếm nâng cao

Bổ sung:

```text
Tìm sinh viên theo ngành
Tìm sinh viên theo trạng thái
Tìm sinh viên theo giới tính
```

## Level 2 - Thống kê nâng cao

Bổ sung:

```text
Tổng số sinh viên
Số sinh viên nam
Số sinh viên nữ
Số sinh viên GPA >= 8
Số sinh viên GPA < 5
```

## Level 3 - Xếp loại

```text
GPA >= 8.0          : Giỏi
7.0 <= GPA < 8.0   : Khá
5.0 <= GPA < 7.0   : Trung bình
GPA < 5.0           : Yếu
```

## Level 4 - Lưu dữ liệu JSON

Cho phép lưu và đọc:

```text
students.json
```

Bổ sung:

```text
Save Data
Load Data
```

## Level 5 - Repository Pattern

Có thể mở rộng:

```text
IStudentRepository
        ↓
StudentRepository
        ↓
StudentService
```

## Level 6 - Interface

Ví dụ:

```csharp
public interface IStudentService
{
    void Add(Student student);
    Student? GetById(string id);
    bool Update(Student student);
    bool Delete(string id);
}
```

---

# 42. Yêu cầu chất lượng code

Sinh viên cần đảm bảo:

1. Không viết toàn bộ chương trình trong `Program.cs`.
2. Không lặp lại code nhập dữ liệu.
3. Phân tách trách nhiệm giữa các lớp.
4. Đặt tên class, method, property rõ ràng.
5. Sử dụng đúng kiểu dữ liệu.
6. Xử lý danh sách rỗng.
7. Kiểm tra `null` khi cần.
8. Kiểm tra tồn tại trước Update/Delete.
9. Sử dụng LINQ phù hợp.
10. Commit Git theo từng chức năng.

Nguyên tắc phân tách:

```text
Student
    ↓
Dữ liệu sinh viên

InputHelper
    ↓
Nhập dữ liệu

StudentValidator
    ↓
Kiểm tra dữ liệu

StudentService
    ↓
Nghiệp vụ

StudentConsoleView
    ↓
Hiển thị / giao tiếp Console

MenuManager
    ↓
Điều khiển menu
```

---

# 43. Tiêu chí đánh giá

| Nội dung | Điểm |
|---|---:|
| Tạo đúng project C# .NET 8 | 0.5 |
| Class `Student` | 1.0 |
| Constructor + Property + Encapsulation | 1.0 |
| `InputHelper` | 1.0 |
| `StudentValidator` | 1.0 |
| CRUD | 2.0 |
| Tìm kiếm + sắp xếp | 1.0 |
| Thống kê | 1.0 |
| Tổ chức source code | 0.5 |
| README + Git | 1.0 |
| **Tổng** | **10.0** |

---

# 44. Deliverables - Bài nộp

Sinh viên cần nộp:

### 1. Source code

Repository GitHub/GitLab chứa toàn bộ source code.

### 2. README.md

README mô tả:

- Mục tiêu.
- Công nghệ.
- Chức năng.
- Cấu trúc project.
- Hướng dẫn chạy.
- Test case.
- Screenshot.

### 3. Screenshot

Các màn hình chức năng chính.

### 4. Git history

Repository cần có lịch sử commit thể hiện quá trình thực hiện bài.

---

# 45. Cấu trúc repository đề xuất

```text
Ex04.StudentManagement/
│
├── README.md
├── Ex04.StudentManagement.sln
│
└── Ex04.StudentManagement/
    │
    ├── Models/
    │   └── Student.cs
    │
    ├── Enums/
    │   ├── Gender.cs
    │   └── StudentStatus.cs
    │
    ├── Helpers/
    │   └── InputHelper.cs
    │
    ├── Validators/
    │   └── StudentValidator.cs
    │
    ├── Services/
    │   └── StudentService.cs
    │
    ├── Views/
    │   └── StudentConsoleView.cs
    │
    ├── Managers/
    │   └── MenuManager.cs
    │
    └── Program.cs
```

---

# 46. Kiến thức cần ghi nhớ

Luồng kiến thức chính:

```text
Class
  ↓
Object
  ↓
Constructor
  ↓
Property
  ↓
Encapsulation
  ↓
List<T>
  ↓
Validation
  ↓
Service
  ↓
CRUD
  ↓
LINQ
  ↓
Search
  ↓
Sort
  ↓
Statistics
```

Đây là nền tảng để tiếp tục học:

```text
OOP nâng cao
      ↓
Generic
      ↓
Collection
      ↓
LINQ
      ↓
Exception Handling
      ↓
File / JSON
      ↓
Async / Await
      ↓
Entity Framework Core
      ↓
ASP.NET Core
      ↓
Web API
      ↓
Database
```

---

# 47. Kết luận

Bài thực hành **Quản lý sinh viên bằng OOP** giúp sinh viên vận dụng các kiến thức C# để xây dựng một ứng dụng Console có cấu trúc rõ ràng.

Trọng tâm không chỉ là làm cho chương trình chạy được mà phải biết:

- Thiết kế Class.
- Tạo Object.
- Sử dụng Constructor.
- Sử dụng Property.
- Áp dụng Encapsulation.
- Sử dụng `List<T>`.
- Kiểm tra dữ liệu đầu vào.
- Tách Validation khỏi nghiệp vụ.
- Tách Service khỏi giao diện Console.
- Sử dụng LINQ.
- Thực hiện CRUD.
- Tìm kiếm.
- Sắp xếp.
- Thống kê.
- Quản lý source code bằng Git.

> **Mục tiêu cuối cùng:** Sinh viên có khả năng xây dựng một ứng dụng C# Console có cấu trúc, có kiểm tra dữ liệu, có nghiệp vụ và có khả năng mở rộng.

---

## Devmaster Academy

### C# .NET 8 Console Application

**BÀI 4 - QUẢN LÝ SINH VIÊN BẰNG OOP**
