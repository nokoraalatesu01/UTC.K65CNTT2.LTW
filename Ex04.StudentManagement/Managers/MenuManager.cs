using Ex04.StudentManagement.Helpers;
using Ex04.StudentManagement.Models;
using Ex04.StudentManagement.Services;
using Ex04.StudentManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.StudentManagement.Managers
{
    public class MenuManager
    {
        private readonly StudentService _studentService;

        private readonly StudentConsoleView _view;

        public MenuManager(
            StudentService studentService,
            StudentConsoleView view)
        {
            _studentService = studentService;
            _view = view;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();

                ShowMenu();

                int choice =
                    InputHelper.ReadInt(
                        "Lựa chọn: ",
                        0,
                        13);

                Console.Clear();

                switch (choice)
                {
                    case 1:
                        AddStudent();
                        break;

                    case 2:
                        DisplayStudents();
                        break;

                    case 3:
                        FindById();
                        break;

                    case 4:
                        SearchByName();
                        break;

                    case 5:
                        UpdateStudent();
                        break;

                    case 6:
                        DeleteStudent();
                        break;

                    case 7:
                        SortByName();
                        break;

                    case 8:
                        SortByGPA();
                        break;

                    case 9:
                        DisplayStudentsGPAFrom8();
                        break;

                    case 10:
                        DisplayTopStudent();
                        break;

                    case 11:
                        DisplayAverageGPA();
                        break;

                    case 12:
                        StatisticsByMajor();
                        break;

                    case 13:
                        StatisticsByStatus();
                        break;

                    case 0:
                        running = false;
                        Console.WriteLine(
                            "Cảm ơn bạn đã sử dụng chương trình!");
                        break;
                }

                if (running)
                {
                    InputHelper.Pause();
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("==============================================================");
            Console.WriteLine("              QUẢN LÝ SINH VIÊN - OOP C#");
            Console.WriteLine("==============================================================");

            Console.WriteLine("1.  Thêm sinh viên");
            Console.WriteLine("2.  Hiển thị danh sách");
            Console.WriteLine("3.  Tìm sinh viên theo mã");
            Console.WriteLine("4.  Tìm gần đúng theo họ tên");
            Console.WriteLine("5.  Cập nhật sinh viên");
            Console.WriteLine("6.  Xóa sinh viên");
            Console.WriteLine("7.  Sắp xếp theo họ tên");
            Console.WriteLine("8.  Sắp xếp theo điểm trung bình");
            Console.WriteLine("9.  Sinh viên có GPA từ 8 trở lên");
            Console.WriteLine("10. Sinh viên có điểm cao nhất");
            Console.WriteLine("11. Tính GPA trung bình");
            Console.WriteLine("12. Thống kê theo ngành");
            Console.WriteLine("13. Thống kê theo trạng thái");
            Console.WriteLine("0.  Thoát");

            Console.WriteLine("==============================================================");
        }

        private void AddStudent()
        {
            Console.WriteLine("===== THÊM SINH VIÊN =====");

            string studentId =
                InputHelper.ReadStudentId("Mã sinh viên: ");

            if (_studentService.GetById(studentId) is not null)
            {
                InputHelper.ShowError($"Mã sinh viên {studentId} đã tồn tại.");

                return;
            }

            Student student =
                _view.InputStudent(studentId);

            bool success =
                _studentService.Add(
                    student,
                    out string message);

            if (success)
            {
                InputHelper.ShowSuccess(message);
            }
            else
            {
                InputHelper.ShowError(message);
            }
        }

        private void DisplayStudents()
        {
            Console.WriteLine("===== DANH SÁCH SINH VIÊN =====");

            _view.DisplayStudents(
                _studentService.GetAll());
        }

        private void FindById()
        {
            Console.WriteLine("===== TÌM SINH VIÊN THEO MÃ =====");

            string studentId =
                InputHelper.ReadStudentId(
                    "Mã sinh viên: ");

            Student? student =
                _studentService.GetById(studentId);

            if (student is null)
            {
                InputHelper.ShowError(
                    $"Không tìm thấy sinh viên {studentId}.");

                return;
            }

            _view.DisplayStudent(student);
        }

        private void SearchByName()
        {
            Console.WriteLine(
                "===== TÌM SINH VIÊN THEO HỌ TÊN =====");

            string keyword =
                InputHelper.ReadNonEmptyString(
                    "Nhập từ khóa: ");

            List<Student> students =
                _studentService.SearchByName(keyword);

            _view.DisplayStudents(students);
        }

        private void UpdateStudent()
        {
            Console.WriteLine("===== CẬP NHẬT SINH VIÊN =====");

            string studentId =
                InputHelper.ReadStudentId(
                    "Mã sinh viên: ");

            Student? existing =
                _studentService.GetById(studentId);

            if (existing is null)
            {
                InputHelper.ShowError(
                    $"Không tìm thấy sinh viên {studentId}.");

                return;
            }

            Console.WriteLine();
            Console.WriteLine("Thông tin hiện tại:");

            _view.DisplayStudent(existing);

            Console.WriteLine();

            Student updatedStudent =
                _view.InputStudent(
                    studentId,
                    true);

            bool success =
                _studentService.Update(
                    updatedStudent,
                    out string message);

            if (success)
            {
                InputHelper.ShowSuccess(message);
            }
            else
            {
                InputHelper.ShowError(message);
            }
        }

        private void DeleteStudent()
        {
            Console.WriteLine("===== XÓA SINH VIÊN =====");

            string studentId =
                InputHelper.ReadStudentId(
                    "Mã sinh viên: ");

            Student? student =
                _studentService.GetById(studentId);

            if (student is null)
            {
                InputHelper.ShowError(
                    $"Không tìm thấy sinh viên {studentId}.");

                return;
            }

            _view.DisplayStudent(student);

            bool confirm =
                InputHelper.ReadYesNo(
                    "Bạn có chắc chắn muốn xóa?");

            if (!confirm)
            {
                Console.WriteLine("Đã hủy thao tác.");
                return;
            }

            bool success =
                _studentService.Delete(
                    studentId,
                    out string message);

            if (success)
            {
                InputHelper.ShowSuccess(message);
            }
            else
            {
                InputHelper.ShowError(message);
            }
        }

        private void SortByName()
        {
            Console.WriteLine(
                "===== SẮP XẾP THEO HỌ TÊN =====");

            _view.DisplayStudents(
                _studentService.SortByName());
        }

        private void SortByGPA()
        {
            Console.WriteLine(
                "===== SẮP XẾP THEO GPA =====");

            _view.DisplayStudents(
                _studentService.SortByGPA());
        }

        private void DisplayStudentsGPAFrom8()
        {
            Console.WriteLine(
                "===== SINH VIÊN GPA >= 8 =====");

            _view.DisplayStudents(
                _studentService.GetStudentsGPAFrom8());
        }

        private void DisplayTopStudent()
        {
            Console.WriteLine(
                "===== SINH VIÊN CÓ GPA CAO NHẤT =====");

            Student? student =
                _studentService.GetTopStudent();

            if (student is null)
            {
                Console.WriteLine("Chưa có sinh viên.");
                return;
            }

            _view.DisplayStudent(student);
        }

        private void DisplayAverageGPA()
        {
            Console.WriteLine(
                "===== GPA TRUNG BÌNH =====");

            double average =
                _studentService.GetAverageGPA();

            Console.WriteLine(
                $"GPA trung bình: {average:F2}");
        }

        private void StatisticsByMajor()
        {
            Console.WriteLine(
                "===== THỐNG KÊ THEO NGÀNH =====");

            Dictionary<string, int> statistics =
                _studentService.StatisticsByMajor();

            _view.DisplayStatistics(statistics);
        }

        private void StatisticsByStatus()
        {
            Console.WriteLine(
                "===== THỐNG KÊ THEO TRẠNG THÁI =====");

            var statistics =
                _studentService.StatisticsByStatus();

            _view.DisplayStatusStatistics(statistics);
        }
    }
}
