using Microsoft.AspNetCore.Mvc;
using Model_Thanh_Vien.Models;

namespace Model_Thanh_Vien.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserListBag()
        {
            var userList = new List<User>
            {
                new User { Id = 1, name = "Nguyen Duc Truong", email = "nguyenductruong@example.com", address = "123 Ninh Binh" },
                new User { Id = 2, name = "Tran Truong Tho", email = "tranthuongtho@example.com", address = "456 Ha Noi" },
                new User { Id = 3, name = "Nguyen Duc Tho", email = "nguyenductho@example.com", address = "789 Da Lat" }
            };
            ViewBag.UserList = userList;
            return View();
        }
        public IActionResult UserListStrong()
        {
            var userList = new List<User>
            {
                new User { Id = 1, name = "Nguyen Duc Truong", email = "nguyenductruong@example.com", address = "123 Ninh Binh" },
                new User { Id = 2, name = "Tran Truong Tho", email = "tranthuongtho@example.com", address = "456 Ha Noi" },
                new User { Id = 3, name = "Nguyen Duc Tho", email = "nguyenductho@example.com", address = "789 Da Lat" }
            };
            return View(userList);
        }
    }
}
