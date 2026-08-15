using Ex04.StudentManagement.Managers;
using Ex04.StudentManagement.Services;
using Ex04.StudentManagement.Validators;
using Ex04.StudentManagement.Views;
using System.Globalization;
using System.Text;
Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;
CultureInfo culture = new CultureInfo("vi-VN");
StudentValidator validator = new();

StudentService studentService = new(validator);

StudentConsoleView view = new();

MenuManager menuManager = new(studentService, view);

// Dữ liệu mẫu
studentService.SeedData();

// Chạy chương trình
menuManager.Run();