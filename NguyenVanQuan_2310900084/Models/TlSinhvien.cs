using System;
using System.Collections.Generic;

namespace NguyenVanQuan_2310900084.Models;

public partial class TlSinhvien
{
    public string MaSv { get; set; } = null!;

    public string? Hosv { get; set; }

    public string? Ten { get; set; }

    public string? Phai { get; set; }

    public DateTime? Ngaysinh { get; set; }

    public string? MaKhoa { get; set; }

    public virtual TlKhoa? MaKhoaNavigation { get; set; }
}
