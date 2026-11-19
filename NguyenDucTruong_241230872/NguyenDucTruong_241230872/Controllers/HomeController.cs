using Microsoft.AspNetCore.Mvc;
using NguyenDucTruong_241230872.Models;
using System.Diagnostics;

namespace NguyenDucTruong_241230872.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLHangHoaContext db;
        public HomeController(QLHangHoaContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var data = db.HangHoas.Where(x => x.Gia >= 100).ToList();
            return View(data);
        }
        public IActionResult HangHoaTheoLoai(int maLoai)
        {
            var data = db.HangHoas.Where(x => x.Gia >= 100 && x.MaLoai == maLoai).ToList();
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
