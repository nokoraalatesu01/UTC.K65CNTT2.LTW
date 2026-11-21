using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NguyenDucTruong_241230872_IT.Models;
using System.Diagnostics;

namespace NguyenDucTruong_241230872_IT.Controllers
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
            var ds = _db.TaiSans.Where(x => x.NguyenGia >= 20).ToList();
            return View(ds);
        }
        public IActionResult TheoLoai ( int id)
        {
            ViewBag.TieuDe = _db.LoaiTaiSans.Find(id)?.TenLoai;
            var ds = _db.TaiSans.Where(x => x.NguyenGia >= 20 && x.MaLoai== id).ToList();
            return PartialView("NguyenDucTruong_MainContent", ds);
        }
        public IActionResult Edit(int id)
        {
            var ts = _db.TaiSans.Find(id);
            if (ts == null) return NotFound();
            ViewBag.DsLoai = new SelectList(_db.LoaiTaiSans, "MaLoai", "TenLoai", ts.MaLoai);
            return View(ts);
        }
        [HttpPost]
        public IActionResult Edit (TaiSan ts)
        {
            if (ModelState.IsValid)
            {
                _db.TaiSans.Update(ts);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DsLoai = new SelectList(_db.LoaiTaiSans, "MaLoai", "TenLoai", ts.MaLoai);
            return View(ts);
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
