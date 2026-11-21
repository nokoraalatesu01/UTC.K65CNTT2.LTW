using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenDucTruong_241230872_IT.Models;

public partial class TaiSan
{
    [Key]
    public int MaTs { get; set; }

    public int MaLoai { get; set; }
    [Required(ErrorMessage ="Ten Khong de trong")]
    public string TenTs { get; set; } = null!;
    [Required(ErrorMessage = "Trong khoang 1-2000")]
    [Range(1.0, 2000.0, ErrorMessage = "Trong khoang 1-2000")]
    public decimal? NguyenGia { get; set; }
    [RegularExpression(@"(?i)^.+\.(jpg|jpeg|png|webp)$", ErrorMessage = "Khong co duoi .jpg | .jpeg | .png | .webp a")]
    public string? Anh { get; set; }

    public virtual LoaiTaiSan MaLoaiNavigation { get; set; } = null!;
}
