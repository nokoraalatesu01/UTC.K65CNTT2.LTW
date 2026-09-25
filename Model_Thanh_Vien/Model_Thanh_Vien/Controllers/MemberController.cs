using Microsoft.AspNetCore.Mvc;
using Model_Thanh_Vien.Models; 

namespace Model_Thanh_Vien.Controllers
{
    public class MemberController : Controller
    {
        public static readonly List<Member> members = new List<Member>()
        {
            new Member { MemberId = Guid.NewGuid().ToString(), Username = "member1", FullName = "Thành viên 1", Password = "123", Email = "tv1@gmail.com" },
            new Member { MemberId = Guid.NewGuid().ToString(), Username = "member2", FullName = "Thành viên 2", Password = "123", Email = "tv2@gmail.com" }
        };
        public IActionResult Index()
        {
            var member = new Member
            {
                MemberId = Guid.NewGuid().ToString(),
                Username = "NguyenDucTruong",
                FullName = "Nguyen Duc Truong",
                Password = "password",
                Email = "nguyenductruong@gmail.com"

            };

            return View(member);
        }

        public IActionResult GetMembers()
        {
            return View(members);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Member mem)
        {
            mem.MemberId = Guid.NewGuid().ToString();
            members.Add(mem);
            return RedirectToAction("GetMembers");
        }
    }
}
