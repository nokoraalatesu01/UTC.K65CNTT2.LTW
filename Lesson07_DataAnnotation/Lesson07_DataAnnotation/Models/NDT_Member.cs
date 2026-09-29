using System.ComponentModel.DataAnnotations;

namespace Lesson07_DataAnnotation.Models
{
    public class NDT_Member
    {

        [Display(Name = "ID")]
        [Required(ErrorMessage = "ID không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "ID phải là số nguyên dương lớn hơn 0")]

        public int Id { get; set; }

        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Tài khoản có độ dài từ 3 đến 20 ký tự")]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu tối thiểu 8 ký tự")]
        public string Password { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage = "Điện thoại không được để trống")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Điện thoại phải gồm 10 chữ số")]
        public string Phone { get; set; }
    }
}