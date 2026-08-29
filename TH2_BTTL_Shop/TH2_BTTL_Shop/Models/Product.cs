namespace TH2_BTTL_Shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public double SalePrice { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        // true = Còn hàng, false = Hết hàng
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
