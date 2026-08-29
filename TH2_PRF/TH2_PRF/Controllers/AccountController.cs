using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;
using System.Xml.Linq;
using TH2_PRF.Models;
using System.Linq;

namespace TH2_PRF.Controllers
{
    /// <summary>
    /// Name : TH2_PRF
    /// Author: Nguyễn Đức Trường
    /// MSSV : 241230872
    /// </summary>
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            List<Account> accounts = new List<Account>
            {
                new Account()
                {
                    Id = 1,
                    Name = "Hoàng Anh",
                    Email = "anh@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/images/Avatar/01.jpg"),
                    Gender = 1, 
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },
                new Account()
                {
                    Id = 2,
                    Name = "Trường Giang",
                    Email = "giang@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/images/Avatar/02.jpg"),
                    Gender = 1, 
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },
                new Account()
                {
                    Id = 3,
                    Name = "Hoàng Thúy",
                    Email = "thuy@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/images/Avatar/03.jpg"),
                    Gender = 1, 
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },
            };
            ViewBag.Accounts = accounts;
            return View();
        }
        // định nghĩa url và nam cho action
        [Route("ho-so-cua-toi", Name ="profile")]
        public IActionResult Profile(int id)
        {
            List<Account> accounts = new List<Account>
        {
        new Account() { Id = 1, Name = "Hoàng Anh",   Email = "anh@gmail.com",   Phone = "0986456789", Address = "Hà Nội", Avatar = Url.Content("~/images/Avatar/01.jpg"), Gender = 1, Bio = "My name is small", Birthday = new DateTime(1998,7,15) },
        new Account() { Id = 2, Name = "Trường Giang", Email = "giang@gmail.com", Phone = "0986456789", Address = "Hà Nội", Avatar = Url.Content("~/images/Avatar/02.jpg"), Gender = 1, Bio = "My name is small", Birthday = new DateTime(1998,7,15) },
        new Account() { Id = 3, Name = "Hoàng Thúy",   Email = "thuy@gmail.com",  Phone = "0986456789", Address = "Hà Nội", Avatar = Url.Content("~/images/Avatar/03.jpg"), Gender = 1, Bio = "My name is small", Birthday = new DateTime(1998,7,15) },
        };
            // gửi đối tượng account qua view
            Account account = accounts.FirstOrDefault(ac => ac.Id == id);
            if (account == null)
            {
                return NotFound(); // hoặc: return RedirectToAction("Index");
            }
            ViewBag.account = account;
            return View();
        }
    }
}
