using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TH_Lab01_QLSV.Data;
using TH_Lab01_QLSV.Models;

namespace TH_Lab01_QLSV.Controllers
{
    public class LearnerController : Controller
    {
        private readonly SchoolContext db;

        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        // Số phần tử trên 1 trang
        private int pageSize = 3;

        // READ
        public IActionResult Index(int? mid)
        {
            IQueryable<Learner> learners = db.Learners.Include(m => m.Major);
            if (mid != null)
            {
                learners = learners.Where(l => l.MajorID == mid);
                ViewBag.mid = mid;
            }

            // Tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;

            // Lấy dữ liệu trang đầu
            var result = learners.OrderBy(l => l.LearnerID).Take(pageSize).ToList();
            return View(result);
        }

        // Phân trang kết hợp lọc và tìm kiếm (gọi bằng AJAX)
        public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
        {
            IQueryable<Learner> learners = db.Learners.Include(m => m.Major);

            // Chỉ số trang, nếu null hoặc <= 0 thì mặc định là 1
            int page = (pageIndex == null || pageIndex <= 0) ? 1 : pageIndex.Value;

            // Lọc theo chuyên ngành
            if (mid != null)
            {
                learners = learners.Where(l => l.MajorID == mid);
                ViewBag.mid = mid;
            }

            // Tìm kiếm theo tên
            if (!string.IsNullOrEmpty(keyword))
            {
                learners = learners.Where(l => l.FirstMidName.ToLower().Contains(keyword.ToLower()));
                ViewBag.keyword = keyword;
            }

            // Tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;

            // Lấy dữ liệu trang hiện tại
            var result = learners.OrderBy(l => l.LearnerID)
                                 .Skip(pageSize * (page - 1))
                                 .Take(pageSize)
                                 .ToList();

            return PartialView("LearnerTable", result);
        }

        public IActionResult LearnerByMajorID(int mid)
        {
            var learners = db.Learners
                .Where(l => l.MajorID == mid)
                .Include(m => m.Major).ToList();
            return PartialView("LearnerTable", learners);
        }

        // CREATE
        public IActionResult Create()
        {
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        // UPDATE
        public IActionResult Edit(int id)
        {
            var learner = db.Learners.Find(id);
            if (learner == null) return NotFound();

            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,
            [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (id != learner.LearnerID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(learner);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(learner.LearnerID)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var learner = db.Learners
                .Include(l => l.Major)
                .Include(e => e.Enrollments)
                .FirstOrDefault(m => m.LearnerID == id);

            if (learner == null) return NotFound();

            if (learner.Enrollments.Count() > 0)
                return Content("This learner has some enrollments, can't delete!");

            return View(learner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var learner = db.Learners.Find(id);
            if (learner != null)
            {
                db.Learners.Remove(learner);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool LearnerExists(int id) => db.Learners.Any(e => e.LearnerID == id);
    }
}