using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenDucTruong_241230872_IT2.Models;

public partial class TaiSan
{
    [Key]
    public int MaTs { get; set; }

    public int MaLoai { get; set; }
    [Required(ErrorMessage = "Khong de Trong")]
    public string TenTs { get; set; } = null!;
    [Required(ErrorMessage ="Phai la khoang tu 1 den 2000")]
    [Range(1.0, 2000.0 ,ErrorMessage = "Phai la khoang tu 1 den 2000")]
    public decimal? NguyenGia { get; set; }
    [RegularExpression(@"(?i)^.+\.(jpg|jpeg|png|webp)$", ErrorMessage = "Duoi phai co dang .jpg | .jpeg | .png | .webp")]
    public string? Anh { get; set; }

    public virtual LoaiTaiSan MaLoaiNavigation { get; set; } = null!;
}
