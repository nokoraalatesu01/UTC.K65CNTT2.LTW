# Đối chiếu yêu cầu bài 4

## Kết quả

Solution đã có đúng 5 project theo kiến trúc đề bài:

- `Domain`: 5 entity và các enum nghiệp vụ.
- `Application`: repository interface, 5 service và 15 báo cáo LINQ.
- `Infrastructure`: lưu JSON, CSV, backup/restore và file log.
- `ConsoleUI`: menu chạy được cho các phân hệ chính.
- `Tests`: 10 test xUnit.

## Đối chiếu chức năng

| Nhóm | Trạng thái | Ghi chú |
|---|---|---|
| Khóa học: CRUD, tìm kiếm, sắp xếp, lọc trạng thái | Đạt | `CourseService` và menu khóa học |
| Khóa học: thống kê học phí | Đạt | `CourseFeeStatisticsAsync`, `TotalCourseFeeAsync` |
| Lớp học: tạo, cập nhật, sĩ số, sắp khai giảng, đang học | Đạt | `ClassService` |
| Lớp học: đóng/hủy | Đạt | `CancelAsync` |
| Học viên: CRUD, tìm kiếm, chống trùng điện thoại/email | Đạt | `StudentService` |
| Học viên: import/export CSV | Đạt | `StudentCsvService` và menu CSV |
| Đăng ký: kiểm tra học viên, lớp, trùng lớp, đủ chỗ | Đạt | `EnrollmentService.AddAsync` |
| Đăng ký: thanh toán, công nợ, hủy | Đạt | `RecordPaymentAsync`, `GetRemainingBalanceAsync`, `CancelAsync` |
| Chăm sóc: lịch sử, hôm nay, quá hạn | Đạt | `CareService` |
| Báo cáo LINQ tối thiểu 10 báo cáo | Đạt | Hiện có 15 phương thức báo cáo |
| JSON, đọc khi khởi động, backup/restore, log lỗi | Đạt | `JsonRepository`, `BackupService`, `FileLogger` |
| Kiểm thử | Đạt | 10/10 test passed |

## Kiểm chứng cuối

```text
dotnet build DevmasterTrainingManagement.sln --no-restore
Build succeeded. 0 Warning(s), 0 Error(s)

dotnet test DevmasterTrainingManagement.Tests/DevmasterTrainingManagement.Tests.csproj --no-build --no-restore
Passed: 10, Failed: 0
```

Lưu ý: lần restore cần kết nối NuGet vì solution sử dụng `CsvHelper` và các package xUnit.
