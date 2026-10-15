using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCoreLA7.Areas.Admin.Data;
using NetCoreLA7.Areas.Admin.Helpers;
using NetCoreLA7.Areas.Admin.Models;

namespace NetCoreLA7.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private const string Folder = "product";
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadCategoriesAsync();
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? imageFile)
        {
            await ValidateAsync(product, imageFile);
            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync(product.CategoryId);
                return View(product);
            }

            product.Image = await ImageHelper.SaveAsync(imageFile, _env.WebRootPath, Folder);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thêm mới sản phẩm thành công";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            await LoadCategoriesAsync(product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile? imageFile)
        {
            var existing = await _context.Products.FindAsync(product.Id);
            if (existing == null) return NotFound();

            await ValidateAsync(product, imageFile);
            if (!ModelState.IsValid)
            {
                product.Image = existing.Image;
                await LoadCategoriesAsync(product.CategoryId);
                return View(product);
            }

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.SalePrice = product.SalePrice;
            existing.Status = product.Status;
            existing.CategoryId = product.CategoryId;
            existing.Description = product.Description;

            if (imageFile != null && imageFile.Length > 0)
            {
                ImageHelper.Delete(existing.Image, _env.WebRootPath, Folder);
                existing.Image = await ImageHelper.SaveAsync(imageFile, _env.WebRootPath, Folder);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật sản phẩm thành công";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ImageHelper.Delete(product.Image, _env.WebRootPath, Folder);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Xóa sản phẩm thành công";
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadCategoriesAsync(int? selectedId = null)
        {
            var categories = await _context.Categories
                .Where(c => c.Status == 1)
                .OrderBy(c => c.Name)
                .ToListAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", selectedId);
        }

        private async Task ValidateAsync(Product product, IFormFile? imageFile)
        {
            if (await _context.Products.AnyAsync(p => p.Id != product.Id && p.Name == product.Name))
                ModelState.AddModelError(nameof(Product.Name), "Tên sản phẩm đã tồn tại");

            if (product.SalePrice > product.Price)
                ModelState.AddModelError(nameof(Product.SalePrice), "Giá khuyến mãi không được lớn hơn giá gốc");

            if (product.CategoryId > 0 && !await _context.Categories.AnyAsync(c => c.Id == product.CategoryId))
                ModelState.AddModelError(nameof(Product.CategoryId), "Danh mục không tồn tại");

            if (imageFile != null && imageFile.Length > 0 && !ImageHelper.IsImage(imageFile))
                ModelState.AddModelError("imageFile", "Chỉ chấp nhận file ảnh (.jpg, .jpeg, .png, .gif, .webp)");
        }
    }
}