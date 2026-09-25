using Microsoft.AspNetCore.Mvc;
using Model_Thanh_Vien.Models;

namespace Model_Thanh_Vien.Models
{
    public class User { 
        public long Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }

    }
}
