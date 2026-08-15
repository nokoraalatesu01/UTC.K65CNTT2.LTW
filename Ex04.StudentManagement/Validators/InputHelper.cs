using System.Globalization;
using System.Text.RegularExpressions;
using Ex04.StudentManagement.Enums;

namespace Ex04.StudentManagement.Helpers;

public static class InputHelper
{
    // =========================================================
    // STRING
    // =========================================================

    public static string ReadNonEmptyString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            string? input = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            ShowError("Dữ liệu không được để trống. Vui lòng nhập lại!");
        }
    }

    // =========================================================
    // STUDENT ID
    // =========================================================

    public static string ReadStudentId(string prompt)
    {
        while (true)
        {
            string studentId = ReadNonEmptyString(prompt);

            // Ví dụ: SV001, SV002...
            if (Regex.IsMatch(studentId, @"^SV\d{3,}$",
                RegexOptions.IgnoreCase))
            {
                return studentId.ToUpper();
            }

            ShowError(
                "Mã sinh viên không hợp lệ! " +
                "Định dạng ví dụ: SV001, SV002...");
        }
    }

    // =========================================================
    // PHONE
    // =========================================================

    public static string ReadPhoneNumber(string prompt)
    {
        while (true)
        {
            string phone = ReadNonEmptyString(prompt);

            if (Regex.IsMatch(phone, @"^\d{9,11}$"))
            {
                return phone;
            }

            ShowError(
                "Số điện thoại không hợp lệ! " +
                "Phải chứa từ 9 đến 11 chữ số.");
        }
    }

    // =========================================================
    // EMAIL
    // =========================================================

    public static string ReadEmail(string prompt)
    {
        while (true)
        {
            string email = ReadNonEmptyString(prompt);

            if (Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$",
                RegexOptions.IgnoreCase))
            {
                return email.ToLower();
            }

            ShowError(
                "Email không đúng định dạng! " +
                "Ví dụ: student@gmail.com");
        }
    }

    // =========================================================
    // INT
    // =========================================================

    public static int ReadInt(
        string prompt,
        int min = int.MinValue,
        int max = int.MaxValue)
    {
        while (true)
        {
            Console.Write(prompt);

            string? input = Console.ReadLine();

            if (int.TryParse(input, out int result) &&
                result >= min &&
                result <= max)
            {
                return result;
            }

            ShowError(
                $"Giá trị không hợp lệ! " +
                $"Vui lòng nhập số nguyên từ {min} đến {max}.");
        }
    }

    // =========================================================
    // DECIMAL
    // =========================================================

    public static decimal ReadDecimal(
        string prompt,
        decimal min = 0,
        decimal max = decimal.MaxValue)
    {
        while (true)
        {
            Console.Write(prompt);

            string? input = Console.ReadLine();

            if (decimal.TryParse(
                    input,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out decimal result) &&
                result >= min &&
                result <= max)
            {
                return result;
            }

            ShowError(
                $"Giá trị không hợp lệ! " +
                $"Vui lòng nhập số trong khoảng [{min} - {max}].");
        }
    }

    // =========================================================
    // DOUBLE
    // =========================================================

    public static double ReadDouble(
        string prompt,
        double min = 0,
        double max = double.MaxValue)
    {
        while (true)
        {
            Console.Write(prompt);

            string? input = Console.ReadLine();

            if (double.TryParse(
                    input,
                    NumberStyles.Float,
                    CultureInfo.CurrentCulture,
                    out double result) &&
                !double.IsNaN(result) &&
                !double.IsInfinity(result) &&
                result >= min &&
                result <= max)
            {
                return result;
            }

            ShowError(
                $"Giá trị không hợp lệ! " +
                $"Vui lòng nhập số trong khoảng [{min} - {max}].");
        }
    }

    // =========================================================
    // DATE
    // =========================================================

    public static DateTime ReadDate(
        string prompt,
        DateTime? minDate = null,
        DateTime? maxDate = null)
    {
        while (true)
        {
            Console.Write(prompt);

            string? input = Console.ReadLine();

            if (DateTime.TryParse(
                    input,
                    CultureInfo.CurrentCulture,
                    DateTimeStyles.None,
                    out DateTime date))
            {
                date = date.Date;

                if (minDate.HasValue &&
                    date < minDate.Value.Date)
                {
                    ShowError(
                        $"Ngày phải từ {minDate.Value:dd/MM/yyyy}.");
                    continue;
                }

                if (maxDate.HasValue &&
                    date > maxDate.Value.Date)
                {
                    ShowError(
                        $"Ngày phải đến {maxDate.Value:dd/MM/yyyy}.");
                    continue;
                }

                return date;
            }

            ShowError(
                "Ngày không hợp lệ! Ví dụ: 15/08/2005");
        }
    }

    // =========================================================
    // ENUM - GENDER
    // =========================================================

    public static Gender ReadGender()
    {
        Console.WriteLine();
        Console.WriteLine("Chọn giới tính:");
        Console.WriteLine("1. Nam");
        Console.WriteLine("2. Nữ");
        Console.WriteLine("3. Khác");

        int choice = ReadInt("Lựa chọn: ", 1, 3);

        return choice switch
        {
            1 => Gender.Male,
            2 => Gender.Female,
            3 => Gender.Other,
            _ => Gender.Other
        };
    }

    // =========================================================
    // ENUM - STUDENT STATUS
    // =========================================================

    public static StudentStatus ReadStudentStatus()
    {
        Console.WriteLine();
        Console.WriteLine("Chọn trạng thái:");
        Console.WriteLine("1. Đang học");
        Console.WriteLine("2. Bảo lưu");
        Console.WriteLine("3. Đã tốt nghiệp");
        Console.WriteLine("4. Thôi học");

        int choice = ReadInt("Lựa chọn: ", 1, 4);

        return choice switch
        {
            1 => StudentStatus.Studying,
            2 => StudentStatus.Reserved,
            3 => StudentStatus.Graduated,
            4 => StudentStatus.DroppedOut,
            _ => StudentStatus.Studying
        };
    }

    // =========================================================
    // YES / NO
    // =========================================================

    public static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (Y/N): ");

            string? input = Console.ReadLine()?.Trim();

            if (string.Equals(input, "Y",
                    StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Equals(input, "N",
                    StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            ShowError("Vui lòng nhập Y hoặc N.");
        }
    }

    // =========================================================
    // ERROR
    // =========================================================

    public static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[LỖI] {message}");
        Console.ResetColor();
    }

    public static void ShowSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[OK] {message}");
        Console.ResetColor();
    }

    public static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Nhấn Enter để tiếp tục...");
        Console.ReadLine();
    }
}