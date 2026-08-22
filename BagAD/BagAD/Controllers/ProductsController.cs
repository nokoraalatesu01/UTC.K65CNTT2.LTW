using BagAD.Models;
using Microsoft.AspNetCore.Mvc;

namespace BagAD.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            List<Product> products = new List<Product>
            {
                new Product
                {
                    ID = 1,
                    Name = "Product 1",
                    Price = 500000,
                    CreatedAt = new DateTime(2020, 12, 25),
                    Image = "/images/product1.jpg"
                },

                new Product
                {
                    ID = 2,
                    Name = "Product 2",
                    Price = 700000,
                    CreatedAt = new DateTime(2020, 12, 25),
                    Image = "/images/product2.jpg"
                },

                new Product
                {
                    ID = 3,
                    Name = "Product 3",
                    Price = 550000,
                    CreatedAt = new DateTime(2020, 12, 25),
                    Image = "/images/product3.jpeg"
                },

                new Product
                {
                    ID = 4,
                    Name = "Product 4",
                    Price = 550000,
                    CreatedAt = new DateTime(2020, 12, 25),
                    Image = "/images/product4.jpg"
                }
            };
            return View(products);
        }
    }
}
