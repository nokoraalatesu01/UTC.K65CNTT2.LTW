using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreLA7.Areas.Admin.Data;
using NetCoreLA7.Areas.Admin.Helpers;
using NetCoreLA7.Areas.Admin.Models;

namespace NetCoreLA7.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private const string Folder = "banner";
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BannerController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Banners
                .OrderByDescending(b => b.Priority)
                .ThenByDescending(b => b.Id)
                .ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Banner());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Banner banner, IFormFile? imageFile)
        {
            await ValidateAsync(banner, imageFile);
            if (!ModelState.IsValid) return View(banner);

            banner.Image = await ImageHelper.SaveAsync(imageFile, _env.WebRootPath, Folder);
            _context.Banners.Add(banner);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thêm mới banner thành công";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner == null) return NotFound();
            return View(banner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Banner banner, IFormFile? imageFile)
        {
            var existing = await _context.Banners.FindAsync(banner.Id);
            if (existing == null) return NotFound();

            await ValidateAsync(banner, imageFile);
            if (!ModelState.IsValid)
            {
                banner.Image = existing.Image;
                return View(banner);
            }

            existing.Name = banner.Name;
            existing.Status = banner.Status;
            existing.Priority = banner.Priority;
            existing.Description = banner.Description;

            if (imageFile != null && imageFile.Length > 0)
            {
                ImageHelper.Delete(existing.Image, _env.WebRootPath, Folder);
                existing.Image = await ImageHelper.SaveAsync(imageFile, _env.WebRootPath, Folder);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật banner thành công";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner == null) return NotFound();

            ImageHelper.Delete(banner.Image, _env.WebRootPath, Folder);
            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Xóa banner thành công";
            return RedirectToAction(nameof(Index));
        }

        private async Task ValidateAsync(Banner banner, IFormFile? imageFile)
        {
            if (await _context.Banners.AnyAsync(b => b.Id != banner.Id && b.Name == banner.Name))
                ModelState.AddModelError(nameof(Banner.Name), "Tên banner đã tồn tại");

            if (imageFile != null && imageFile.Length > 0 && !ImageHelper.IsImage(imageFile))
                ModelState.AddModelError("imageFile", "Chỉ chấp nhận file ảnh (.jpg, .jpeg, .png, .gif, .webp)");
        }
    }
}