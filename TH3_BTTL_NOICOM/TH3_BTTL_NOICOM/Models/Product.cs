using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TH3_BTTL_NOICOM.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }

        // Danh sách sản phẩm mẫu
        public List<Product> GetProductList()
        {
            List<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Image = "/images/products/NoiCom.jpg", Price = 2850000 },
                new Product { Id = 2, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Image = "/images/products/NoiCom.jpg", Price = 2850000 },
                new Product { Id = 3, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Image = "/images/products/NoiCom.jpg", Price = 2850000 },
            };
            return products;
        }
        public List<SelectListItem> Categories { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Áo dài" },
            new SelectListItem { Value = "2", Text = "Áo đông" },
            new SelectListItem { Value = "3", Text = "Túi xách" },
            new SelectListItem { Value = "4", Text = "Đồng hồ" },
            new SelectListItem { Value = "5", Text = "Ví da" },
            new SelectListItem { Value = "6", Text = "Thắt lưng da" },
            new SelectListItem { Value = "7", Text = "Tủ lạnh" },
            new SelectListItem { Value = "8", Text = "Tivi" },
            new SelectListItem { Value = "9", Text = "Quạt điện" },
            new SelectListItem { Value = "10", Text = "Lò sưởi" },
        };
    }
}