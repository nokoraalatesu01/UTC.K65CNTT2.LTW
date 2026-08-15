using Ex04.StudentManagement.Enums;
using Ex04.StudentManagement.Models;
using Ex04.StudentManagement.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Ex04.StudentManagement.Services
{
    /// <summary>
    /// Name: StudentService
    /// Author: Chung  Trinhj
    /// </summary>
    public class StudentService
    {
        private readonly List<Student> _students = new();

        private readonly StudentValidator _validator;
        public StudentService(StudentValidator validator)
        {
            _validator = validator;
        }
        // =========================================================
        // CREATE
        // =========================================================

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

        // =========================================================
        // READ ALL
        // =========================================================

        public IReadOnlyList<Student> GetAll()
        {
            return _students.AsReadOnly();
        }

        // =========================================================
        // FIND BY ID
        // =========================================================
        
        public Student? GetById(string studentId)
        {
            return _students.FirstOrDefault(s =>
                s.studentId.Equals(
                    studentId,
                    StringComparison.OrdinalIgnoreCase));
        }
        // =========================================================
        // SEARCH BY NAME
        // =========================================================

        public List<Student> SearchByName(string keyword)
        {
            return _students
                .Where(s =>
                    s.fullName.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // =========================================================
        // UPDATE
        // =========================================================

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

        // =========================================================
        // DELETE
        // =========================================================

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

        // =========================================================
        // SORT BY NAME
        // =========================================================

        public List<Student> SortByName()
        {
            return _students
                .OrderBy(s => s.fullName)
                .ToList();
        }

        // =========================================================
        // SORT BY GPA
        // =========================================================

        public List<Student> SortByGPA()
        {
            return _students
                .OrderByDescending(s => s.gpa)
                .ToList();
        }

        // =========================================================
        // GPA >= 8
        // =========================================================

        public List<Student> GetStudentsGPAFrom8()
        {
            return _students
                .Where(s => s.gpa >= 8)
                .OrderByDescending(s => s.gpa)
                .ToList();
        }

        // =========================================================
        // TOP STUDENT
        // =========================================================

        public Student? GetTopStudent()
        {
            return _students
                .OrderByDescending(s => s.gpa)
                .FirstOrDefault();
        }

        // =========================================================
        // AVERAGE GPA
        // =========================================================

        public double GetAverageGPA()
        {
            if (_students.Count == 0)
            {
                return 0;
            }

            return _students.Average(s => s.gpa);
        }

        // =========================================================
        // STATISTICS BY MAJOR
        // =========================================================

        public Dictionary<string, int> StatisticsByMajor()
        {
            return _students
                .GroupBy(s => s.major)
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count());
        }

        // =========================================================
        // STATISTICS BY STATUS
        // =========================================================

        public Dictionary<StudentStatus, int> StatisticsByStatus()
        {
            return _students
                .GroupBy(s => s.status)
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count());
        }

        // =========================================================
        // SAMPLE DATA
        // =========================================================

        public void SeedData()
        {
            Add(
                new Student()
                {
                    studentId = "SV001",
                    fullName = "Trịnh Văn Chung",
                    dateOfBirth = new DateTime(1979, 5, 25),
                    gender = Gender.Male,
                    email = "chungtrinhj@gmail.com",
                    phoneNumber = "0978611889",
                    major = "Công nghệ thông tin",
                    gpa = 8.5,
                    status = StudentStatus.Studying,
                }, out _);

            Add(
                new Student()
                {
                    studentId = "SV002",
                    fullName = "Nguyễn Văn An",
                    dateOfBirth = new DateTime(2005, 5, 10),
                    gender = Gender.Male,
                    email = "annguyen@gmail.com",
                    phoneNumber = "0988111222",
                    major = "Công nghệ thông tin",
                    gpa = 8.5,
                    status = StudentStatus.Studying
                }, out _);
            Add(
                new Student()
                {
                    studentId = "SV003",
                    fullName = "Nguyễn Thị Minh Ái",
                    dateOfBirth = new DateTime(2005, 5, 10),
                    gender = Gender.Male,
                    email = "aiminh@gmail.com",
                    phoneNumber = "0988111333",
                    major = "Công nghệ thông tin",
                    gpa = 9.5,
                    status = StudentStatus.Studying
                }, out _);
        }
    }
}
