using Bai01.QuanLySinhVien.Managers;
using Bai01.QuanLySinhVien.Services;
using Bai01.QuanLySinhVien.Validators;
using Bai01.QuanLySinhVien.Views;
using System.Globalization;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;
CultureInfo culture = new CultureInfo("vi-VN");

StudentValidator validator = new();

StudentService studentService = new(validator);

StudentConsoleView view = new();

MenuManager menuManager = new(studentService, view);

studentService.SeedData();

menuManager.Run();
