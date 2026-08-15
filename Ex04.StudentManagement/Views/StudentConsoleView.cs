using Ex04.StudentManagement.Enums;
using Ex04.StudentManagement.Helpers;
using Ex04.StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.StudentManagement.Views
{
    public class StudentConsoleView
    {
        public Student InputStudent(string studentId, bool isUpdate = false)
        {
            Student student = new Student();
            Console.WriteLine();
            Console.WriteLine(
                isUpdate
                    ? "===== CẬP NHẬT SINH VIÊN ====="
                    : "===== THÊM SINH VIÊN =====");
            student.studentId = studentId;
            student.fullName =
                InputHelper.ReadNonEmptyString(
                    "Họ tên: ");

            student.dateOfBirth =
                InputHelper.ReadDate(
                    "Ngày sinh (dd/MM/yyyy): ",
                    new DateTime(1950, 1, 1),
                    DateTime.Today);

            student.gender =
                InputHelper.ReadGender();

            student.email =
                InputHelper.ReadEmail("Email: ");

            student.phoneNumber =
                InputHelper.ReadPhoneNumber(
                    "Số điện thoại: ");

            student.major =
                InputHelper.ReadNonEmptyString(
                    "Ngành học: ");

            student.gpa =
                InputHelper.ReadDouble(
                    "Điểm trung bình: ",
                    0,
                    10);

            student.status =
                InputHelper.ReadStudentStatus();


            return student;
        }
        public void DisplayStudent(Student student)
        {
            Console.WriteLine(
                $"Mã SV       : {student.studentId}");

            Console.WriteLine(
                $"Họ tên      : {student.fullName}");

            Console.WriteLine(
                $"Ngày sinh   : {student.dateOfBirth:dd/MM/yyyy}");

            Console.WriteLine(
                $"Giới tính   : {GetGenderName(student.gender)}");

            Console.WriteLine(
                $"Email       : {student.email}");
            
            Console.WriteLine(
                $"Điện thoại  : {student.phoneNumber}");

            Console.WriteLine(
                $"Ngành       : {student.major}");

            Console.WriteLine(
                $"GPA         : {student.gpa:F2}");

            Console.WriteLine(
                $"Trạng thái  : {GetStatusName(student.status)}");
        }
        private string GetGenderName(Gender gender)
        {
            return gender switch
            {
                Gender.Male => "Nam",
                Gender.Female => "Nữ",
                Gender.Other => "Khác",
                _ => "Không xác định"
            };
        }
        private string GetStatusName(StudentStatus status)
        {
            return status switch
            {
                StudentStatus.Studying => "Đang học",
                StudentStatus.Reserved => "Bảo lưu",
                StudentStatus.Graduated => "Đã tốt nghiệp",
                StudentStatus.DroppedOut => "Thôi học",
                _ => "Không xác định"
            };
        }
        public void DisplayStudents(
        IEnumerable<Student> students)
        {
            List<Student> list = students.ToList();

            if (list.Count == 0)
            {
                Console.WriteLine("Không có dữ liệu.");
                return;
            }

            Console.WriteLine(
                "-------------------------------------------------------------------------------------------------------------");

            Console.WriteLine(
                $"{"Mã",-8} | {"Họ tên",-25} | {"Ngày sinh",-12} | " +
                $"{"Email",-25} | {"Ngành",-20} | {"GPA",-6} | {"Trạng thái"}");

            Console.WriteLine(
                "-------------------------------------------------------------------------------------------------------------");

            foreach (Student student in list)
            {
                Console.WriteLine(
                    $"{student.studentId,-8} | " +
                    $"{student.fullName,-25} | " +
                    $"{student.dateOfBirth:dd/MM/yyyy,-12} | " +
                    $"{student.email,-25} | " +
                    $"{student.major,-20} | " +
                    $"{student.gpa,-6:F2} | " +
                    $"{GetStatusName(student.status)}");
            }

            Console.WriteLine(
                "-------------------------------------------------------------------------------------------------------------");

            Console.WriteLine(
                $"Tổng số sinh viên: {list.Count}");
        }


        /// <summary>
        /// Các phương thực phục vụ thống kê
        /// </summary>
        /// <param name="statistics"></param>
        public void DisplayStatistics(Dictionary<string, int> statistics)
        {
            if (statistics.Count == 0)
            {
                Console.WriteLine("Không có dữ liệu.");
                return;
            }

            foreach (var item in statistics)
            {
                Console.WriteLine(
                    $"{item.Key,-30}: {item.Value}");
            }
        }

        public void DisplayStatusStatistics(Dictionary<StudentStatus, int> statistics)
        {
            if (statistics.Count == 0)
            {
                Console.WriteLine("Không có dữ liệu.");
                return;
            }

            foreach (var item in statistics)
            {
                Console.WriteLine(
                    $"{GetStatusName(item.Key),-20}: {item.Value}");
            }
        }
    }
}
