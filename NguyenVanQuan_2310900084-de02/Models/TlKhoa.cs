using System;
using System.Collections.Generic;

namespace NguyenVanQuan_2310900084.Models;

public partial class TlKhoa
{
    public string MaKhoa { get; set; } = null!;

    public string? TenKhoa { get; set; }

    public virtual ICollection<TlSinhvien> TlSinhviens { get; set; } = new List<TlSinhvien>();
}
