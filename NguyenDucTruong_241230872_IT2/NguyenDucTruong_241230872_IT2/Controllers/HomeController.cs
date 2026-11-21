using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NguyenDucTruong_241230872_IT2.Models;
using System.Diagnostics;

namespace NguyenDucTruong_241230872_IT2.Controllers
{
    public class HomeController : Controller
    {
        private readonly QltaiSanContext _db;
        public HomeController(QltaiSanContext db)
        {
            _db = db;
        } 
        public IActionResult Index()
        {
            var ds = _db.TaiSans.Where(t => t.NguyenGia >= 20).ToList();
            return View(ds);
        }
        public IActionResult TheoLoai (int id)
        {
            var ds = _db.TaiSans.Where(t => t.NguyenGia >= 20 && t.MaLoai == id).ToList();
            return PartialView("NguyenDucTruong_MainContent",ds);
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
