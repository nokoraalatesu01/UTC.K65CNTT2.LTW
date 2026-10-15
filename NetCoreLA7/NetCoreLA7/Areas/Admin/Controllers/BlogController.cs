using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreLA7.Areas.Admin.Data;
using NetCoreLA7.Areas.Admin.Helpers;
using NetCoreLA7.Areas.Admin.Models;

namespace NetCoreLA7.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private const string Folder = "blog";
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Blogs.OrderByDescending(b => b.Id).ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Blog());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog, IFormFile? imageFile)
        {
            await ValidateAsync(blog, imageFile);
            if (!ModelState.IsValid) return View(blog);

            blog.Image = await ImageHelper.SaveAsync(imageFile, _env.WebRootPath, Folder);
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thêm mới bài viết thành công";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Blog blog, IFormFile? imageFile)
        {
            var existing = await _context.Blogs.FindAsync(blog.Id);
            if (existing == null) return NotFound();

            await ValidateAsync(blog, imageFile);
            if (!ModelState.IsValid)
            {
                blog.Image = existing.Image;
                return View(blog);
            }

            existing.Name = blog.Name;
            existing.Status = blog.Status;
            existing.Description = blog.Description;

            if (imageFile != null && imageFile.Length > 0)
            {
                ImageHelper.Delete(existing.Image, _env.WebRootPath, Folder);
                existing.Image = await ImageHelper.SaveAsync(imageFile, _env.WebRootPath, Folder);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật bài viết thành công";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return NotFound();

            ImageHelper.Delete(blog.Image, _env.WebRootPath, Folder);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Xóa bài viết thành công";
            return RedirectToAction(nameof(Index));
        }

        private async Task ValidateAsync(Blog blog, IFormFile? imageFile)
        {
            if (await _context.Blogs.AnyAsync(b => b.Id != blog.Id && b.Name == blog.Name))
                ModelState.AddModelError(nameof(Blog.Name), "Tên bài viết đã tồn tại");

            if (imageFile != null && imageFile.Length > 0 && !ImageHelper.IsImage(imageFile))
                ModelState.AddModelError("imageFile", "Chỉ chấp nhận file ảnh (.jpg, .jpeg, .png, .gif, .webp)");
        }
    }
}