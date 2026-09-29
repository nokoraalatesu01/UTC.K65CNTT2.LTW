using Lesson07_DataAnnotation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Lesson07_DataAnnotation.Controllers
{
    public class NDT_MemberController : Controller
    {   
        private static List<NDT_Member> members = new List<NDT_Member>();
        public IActionResult Index()
        {
            return View(members);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NDT_Member member)
        {
            if (!ModelState.IsValid)
            {
                return View(member);
            }
            member.Id = members.Count + 1;
            members.Add(member);
            return RedirectToAction(nameof(Index));
        }
    }
}
