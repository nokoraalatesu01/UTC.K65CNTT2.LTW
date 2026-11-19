using Microsoft.AspNetCore.Mvc;
using NguyenTruong_241230872.Models;
using System.Diagnostics;

namespace NguyenTruong_241230872.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLNhaSachContext db;

        public HomeController(QLNhaSachContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var data = db.Saches.Where(x => x.Gia >= 100).ToList();
            return View(data);
        }

        public IActionResult SachTheoLoai(int maTl)
        {
            var data = db.Saches.Where(x => x.Gia >= 100 && x.MaTl == maTl).ToList();
            return PartialView("NguyenDucTruong_MainContent",data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
