using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreLA7.Areas.Admin.Models
{
    public class Banner
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên banner không được để trống")]
        [StringLength(100, ErrorMessage = "Tên banner tối đa 100 ký tự")]
        public string Name { get; set; } = string.Empty;

        public byte Status { get; set; } = 1;

        public int Priority { get; set; } = 0;

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? Image { get; set; }

        [StringLength(350, ErrorMessage = "Mô tả tối đa 350 ký tự")]
        public string? Description { get; set; }
    }
}