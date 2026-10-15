using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreLA7.Areas.Admin.Data;
using NetCoreLA7.Areas.Admin.Helpers;
using NetCoreLA7.Areas.Admin.Models;

namespace NetCoreLA7.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private const string Folder = "category";
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Categories.OrderByDescending(c => c.Id).ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category, IFormFile? imageFile)
        {
            await ValidateAsync(category, imageFile);
            if (!ModelState.IsValid) return View(category);

            category.Image = await ImageHelper.SaveAsync(imageFile, _env.WebRootPath, Folder);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thêm mới danh mục thành công";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category, IFormFile? imageFile)
        {
            var existing = await _context.Categories.FindAsync(category.Id);
            if (existing == null) return NotFound();

            await ValidateAsync(category, imageFile);
            if (!ModelState.IsValid)
            {
                category.Image = existing.Image; // để view còn hiện ảnh cũ
                return View(category);
            }

            existing.Name = category.Name;
            existing.Status = category.Status;
            existing.Description = category.Description;

            if (imageFile != null && imageFile.Length > 0)
            {
                ImageHelper.Delete(existing.Image, _env.WebRootPath, Folder);
                existing.Image = await ImageHelper.SaveAsync(imageFile, _env.WebRootPath, Folder);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật danh mục thành công";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            if (await _context.Products.AnyAsync(p => p.CategoryId == id))
            {
                TempData["Error"] = "Không thể xóa: danh mục này đang có sản phẩm";
                return RedirectToAction(nameof(Index));
            }

            ImageHelper.Delete(category.Image, _env.WebRootPath, Folder);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Xóa danh mục thành công";
            return RedirectToAction(nameof(Index));
        }

        private async Task ValidateAsync(Category category, IFormFile? imageFile)
        {
            if (await _context.Categories.AnyAsync(c => c.Id != category.Id && c.Name == category.Name))
                ModelState.AddModelError(nameof(Category.Name), "Tên danh mục đã tồn tại");

            if (imageFile != null && imageFile.Length > 0 && !ImageHelper.IsImage(imageFile))
                ModelState.AddModelError("imageFile", "Chỉ chấp nhận file ảnh (.jpg, .jpeg, .png, .gif, .webp)");
        }
    }
}