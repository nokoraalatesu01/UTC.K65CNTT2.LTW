using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TH2_BTTL_Shop.Models;

namespace TH2_BTTL_Shop.Controllers
{
    public class ProductController : Controller
    {
        // Danh sách danh mục dùng chung cho các action
        private List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category { Id = 1, Name = "Quần áo" },
                new Category { Id = 2, Name = "Túi xách" },
                new Category { Id = 3, Name = "Đồng hồ" },
                new Category { Id = 4, Name = "Tivi" },
                new Category { Id = 5, Name = "Tủ lạnh" },
                new Category { Id = 6, Name = "Máy bơm" },
                new Category { Id = 7, Name = "Quạt điện" },
                new Category { Id = 8, Name = "Lò sưởi" },
            };
        }

        // Danh sách sản phẩm dùng chung cho các action
        // Lưu ý: ảnh đang dùng tạm ảnh trong thư mục Avatar để demo,
        // bạn hãy tạo thư mục wwwroot/images/Products và đổi lại đường dẫn ảnh cho đúng sản phẩm thật.
        private List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Bộ đồ bơi cho trẻ em nam",
                    Image = Url.Content("~/images/Avatar/01.jpg"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde iure doloribus natus expedita.",
                    Status = true,
                    CreatedAt = new DateTime(2021, 7, 15)
                },
                new Product
                {
                    Id = 2,
                    Name = "Bộ đồ bơi cho trẻ em nữ",
                    Image = Url.Content("~/images/Avatar/02.jpg"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde iure doloribus natus expedita.",
                    Status = true,
                    CreatedAt = new DateTime(2021, 7, 15)
                },
                new Product
                {
                    Id = 3,
                    Name = "Bộ đồ bơi cho trẻ em từ 3-5 tuổi",
                    Image = Url.Content("~/images/Avatar/03.jpg"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde iure doloribus natus expedita.",
                    Status = true,
                    CreatedAt = new DateTime(2021, 7, 15)
                },
                new Product
                {
                    Id = 4,
                    Name = "Bộ đồ bơi cho trẻ em thời trang",
                    Image = Url.Content("~/images/Avatar/04.jpg"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde iure doloribus natus expedita.",
                    Status = false,
                    CreatedAt = new DateTime(2021, 7, 15)
                },
                new Product
                {
                    Id = 5,
                    Name = "Túi thời trang mẫu mới 2021",
                    Image = Url.Content("~/images/Avatar/05.jpg"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 2,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde iure doloribus natus expedita.",
                    Status = true,
                    CreatedAt = new DateTime(2021, 7, 15)
                },
                new Product
                {
                    Id = 6,
                    Name = "Túi thời trang da cá sấu",
                    Image = Url.Content("~/images/Avatar/06.jpg"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 2,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde iure doloribus natus expedita.",
                    Status = true,
                    CreatedAt = new DateTime(2021, 7, 15)
                },
            };
        }

        // Đổi route mặc định /Product thành /san-pham
        [Route("san-pham", Name = "product-index")]
        public IActionResult Index(int? categoryId)
        {
            List<Category> categories = GetCategories();
            List<Product> products = GetProducts();

            // Nếu có chọn danh mục thì lọc sản phẩm theo categoryId
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
            }

            ViewBag.Categories = categories;
            ViewBag.Products = products;
            ViewBag.CurrentCategoryId = categoryId;

            return View();
        }

        // Trang chi tiết sản phẩm, truy cập dạng /san-pham/chi-tiet?id=1
        [Route("san-pham/chi-tiet", Name = "product-detail")]
        public IActionResult Detail(int id)
        {
            Product product = GetProducts().FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Product = product;
            return View();
        }
    }
}
