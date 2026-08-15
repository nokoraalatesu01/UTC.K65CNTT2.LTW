using Bai01.QuanLySinhVien.Enums;
using Bai01.QuanLySinhVien.Models;
using Bai01.QuanLySinhVien.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Bai01.QuanLySinhVien.Services
{
    public class StudentService
    {
        private readonly List<Student> _students = new();

        private readonly StudentValidator _validator;
        public StudentService(StudentValidator validator)
        {
            _validator = validator;
        }

        public bool Add(
            Student student,
            out string message)
        {
            if (!_validator.IsValid(student, out message))
            {
                return false;
            }

            bool exists = _students.Any(s =>
                s.studentId.Equals(
                    student.studentId,
                    StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                message = $"Mã sinh viên '{student.studentId}' đã tồn tại.";
                return false;
            }

            _students.Add(student);

            message = "Thêm sinh viên thành công.";
            return true;
        }


        public IReadOnlyList<Student> GetAll()
        {
            return _students.AsReadOnly();
        }

        
        public Student? GetById(string studentId)
        {
            return _students.FirstOrDefault(s =>
                s.studentId.Equals(
                    studentId,
                    StringComparison.OrdinalIgnoreCase));
        }

        public List<Student> SearchByName(string keyword)
        {
            return _students
                .Where(s =>
                    s.fullName.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }


        public bool Update( Student updatedStudent,out string message)
        {
            if (!_validator.IsValid(
                    updatedStudent,
                    out message))
            {
                return false;
            }

            Student? existing =
                GetById(updatedStudent.studentId);

            if (existing is null)
            {
                message =
                    $"Không tìm thấy sinh viên có mã " +
                    $"{updatedStudent.studentId}.";

                return false;
            }

            existing.Update(
                updatedStudent.fullName,
                updatedStudent.dateOfBirth,
                updatedStudent.gender,
                updatedStudent.email,
                updatedStudent.phoneNumber,
                updatedStudent.major,
                updatedStudent.gpa,
                updatedStudent.status);

            message = "Cập nhật sinh viên thành công.";
            return true;
        }


        public bool Delete(
            string studentId,
            out string message)
        {
            Student? student = GetById(studentId);

            if (student is null)
            {
                message =
                    $"Không tìm thấy sinh viên có mã {studentId}.";

                return false;
            }

            _students.Remove(student);

            message = "Xóa sinh viên thành công.";
            return true;
        }


        public List<Student> SortByName()
        {
            return _students
                .OrderBy(s => s.fullName)
                .ToList();
        }


        public List<Student> SortByGPA()
        {
            return _students
                .OrderByDescending(s => s.gpa)
                .ToList();
        }


        public List<Student> GetStudentsGPAFrom8()
        {
            return _students
                .Where(s => s.gpa >= 8)
                .OrderByDescending(s => s.gpa)
                .ToList();
        }


        public Student? GetTopStudent()
        {
            return _students
                .OrderByDescending(s => s.gpa)
                .FirstOrDefault();
        }


        public double GetAverageGPA()
        {
            if (_students.Count == 0)
            {
                return 0;
            }

            return _students.Average(s => s.gpa);
        }


        public Dictionary<string, int> StatisticsByMajor()
        {
            return _students
                .GroupBy(s => s.major)
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count());
        }


        public Dictionary<StudentStatus, int> StatisticsByStatus()
        {
            return _students
                .GroupBy(s => s.status)
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count());
        }


        public void SeedData()
        {
            Add(
                new Student()
                {
                    studentId = "SV001",
                    fullName = "Nguyễn Đức Trường",
                    dateOfBirth = new DateTime(2004, 05, 05),
                    gender = Gender.Male,
                    email = "a@gmail.com",
                    phoneNumber = "0123456789",
                    major = "Công nghệ thông tin",
                    gpa = 8.5,
                    status = StudentStatus.Studying,
                }, out _);

            Add(
                new Student()
                {
                    studentId = "SV002",
                    fullName = "Nguyễn Đức Tài",
                    dateOfBirth = new DateTime(2005, 05, 10),
                    gender = Gender.Male,
                    email = "b@gmail.com",
                    phoneNumber = "0987654321",
                    major = "Công nghệ thông tin",
                    gpa = 8.5,
                    status = StudentStatus.Studying
                }, out _);
            Add(
                new Student()
                {
                    studentId = "SV003",
                    fullName = "Nguyễn Thị Ngọc Anh",
                    dateOfBirth = new DateTime(2006, 07, 31),
                    gender = Gender.Female,
                    email = "c@gmail.com",
                    phoneNumber = "0335363882",
                    major = "Quản trị kinh doanh",
                    gpa = 9.5,
                    status = StudentStatus.Studying
                }, out _);
        }
    }
}
