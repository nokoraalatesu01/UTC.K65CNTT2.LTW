using Ex04.StudentManagement.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.StudentManagement.Models
{
    public class Student
    {
        public string studentId { get; set; } = string.Empty;
        public string fullName { get; set; }=string.Empty;
        public DateTime dateOfBirth { get; set; }

        public Gender gender { get; set; }
        public string email { get; set; } = string.Empty;
        public string phoneNumber {  get; set; }  = string.Empty;
        public string major { get; set; } = string.Empty;
        public double gpa { get; set; }
        public StudentStatus status { get; set; }
        public override string ToString()
        {
            return
                $"{studentId,-8} | " +
                $"{fullName,-25} | " +
                $"{dateOfBirth:dd/MM/yyyy} | " +
                $"{gender,-8} | " +
                $"{email,-30} | " +
                $"{phoneNumber,-12} | " +
                $"{major,-20} | " +
                $"{gpa,5:F2} | " +
                $"{status}";
        }

        public void Update(
        string fullName,
        DateTime dateOfBirth,
        Gender gender,
        string email,
        string phoneNumber,
        string major,
        double gpa,
        StudentStatus status)
        {
            this.fullName = fullName;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.major = major;
            this.gpa = gpa;
            this.status = status;
        }
    }
}
