using System.ComponentModel.DataAnnotations;

namespace Model_Thanh_Vien.Models
{
    public class Login
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
