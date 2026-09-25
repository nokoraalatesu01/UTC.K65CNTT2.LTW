using Microsoft.AspNetCore.Mvc;
using Model_Thanh_Vien.Models;

namespace Model_Thanh_Vien.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Loginprimitive()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginPrimitive (string userName, string password)
        {
            if (userName == "Admin" && password == "123")
            {   
                string msg = "Xin chào " + userName;
                return Content(msg);
            }
            else
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng";
                return View();
            }
        }

        [HttpGet]
        public IActionResult LoginObject()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginObject(Login login)
        {
            if (login.Username == "Admin" && login.Password == "123")
            {
                return Content("Xin chào " + login.Username);
            }
            ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng";
            return View(login);
        }
    }
}
