using System.ComponentModel.DataAnnotations;
namespace BTTL_Lab05.Models
{
    public class Product
    {
        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        public int Id { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Tên sản phẩm phải từ 6 đến 150 ký tự")]
        public string Name { get; set; }

        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }

        [Display(Name = "Giá chuẩn")]
        [Required(ErrorMessage = "Giá chuẩn không được để trống")]
        [Range(100000, double.MaxValue, ErrorMessage = "Giá chuẩn phải nhỏ nhất là 100,000")]
        public float Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        [Required(ErrorMessage = "Giá khuyến mãi không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá khuyến mãi không được là số âm")]
        public float SalePrice { get; set; }

        [Display(Name = "Danh mục sản phẩm")]
        [Required(ErrorMessage = "Vui lòng chọn danh mục sản phẩm")]
        public int CategoryId { get; set; }

        [Display(Name = "Mô tả sản phẩm")]
        [Required(ErrorMessage = "Mô tả sản phẩm không được để trống")]
        [StringLength(1500, ErrorMessage = "Mô tả không được vượt quá 1500 ký tự")]
        [NoSensitiveWords]
        public string Description { get; set; }


    }
}
