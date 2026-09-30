using Microsoft.AspNetCore.Mvc;
using BTTL_Lab05.Models;

namespace BTTL_Lab05.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public ProductController(IWebHostEnvironment env)
        {
            _env = env;
        }
        private static List<Category> categories = new List<Category>
         {
            new Category { Id = 1, Name = "Điện thoại" },
            new Category { Id = 2, Name = "Laptop" },
            new Category { Id = 3, Name = "Tablet" },
            new Category { Id = 4, Name = "Phụ kiện" }
        };

        private static List<Product> products = new List<Product>();
        public IActionResult Index()
        {
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product, IFormFile? imageFile)
        {
            if (product.SalePrice >= product.Price * 0.9f)
                ModelState.AddModelError("SalePrice", "Giá khuyến mãi phải nhỏ hơn giá chuẩn 10%");

            if (imageFile == null || imageFile.Length == 0)
                ModelState.AddModelError("Image", "Hình ảnh sản phẩm phải được chọn và tải lên");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = categories;
                return View(product);
            }

            string uploadsFolder = Path.Combine(_env.WebRootPath, "products");
            Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid() + "_" + Path.GetFileName(imageFile!.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }

            product.Image = "/products/" + uniqueFileName;
            products.Add(product);
            return RedirectToAction(nameof(Index));
        }


    }
}
