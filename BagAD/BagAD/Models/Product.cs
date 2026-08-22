using Microsoft.AspNetCore.Mvc;

namespace BagAD.Models
{
    public class Product : Controller
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Image { get; set; }
    }
}
