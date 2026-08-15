using Ex04.StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ex04.StudentManagement.Validators
{
    public class StudentValidator
    {
        public bool IsValid(Student student, out string errorMessage)
        {
            if (student is null)
            {
                errorMessage = "Sinh viên không được null.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(student.studentId))
            {
                errorMessage = "Mã sinh viên không được để trống.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(student.fullName))
            {
                errorMessage = "Họ tên không được để trống.";
                return false;
            }

            if (student.gpa < 0 || student.gpa > 10)
            {
                errorMessage = "Điểm trung bình phải từ 0 đến 10.";
                return false;
            }

            if (!IsValidEmail(student.email))
            {
                errorMessage = "Email không đúng định dạng.";
                return false;
            }

            if (!IsValidPhone(student.phoneNumber))
            {
                errorMessage = "Số điện thoại phải có từ 9 đến 11 chữ số.";
                return false;
            }

            if (student.dateOfBirth.Date > DateTime.Today)
            {
                errorMessage = "Ngày sinh không được lớn hơn ngày hiện tại.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$",
                RegexOptions.IgnoreCase);
        }

        private bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^\d{9,11}$");
        }
    }
}
