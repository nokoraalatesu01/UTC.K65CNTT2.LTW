using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NetCoreLA7.Areas.Admin.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100, ErrorMessage = "Tên danh mục tối đa 100 ký tự")]
        public string Name { get; set; } = string.Empty;

        public byte Status { get; set; } = 1;

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Today;

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? Image { get; set; }

        [StringLength(350, ErrorMessage = "Mô tả tối đa 350 ký tự")]
        public string? Description { get; set; }

        [ValidateNever]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}