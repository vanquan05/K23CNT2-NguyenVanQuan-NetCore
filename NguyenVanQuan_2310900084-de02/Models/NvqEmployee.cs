using System;
using System.Collections.Generic;

namespace NguyenVanQuan_2310900084.Models;

public partial class NvqEmployee
{
    public int NvqEmpId { get; set; }

    public string NvqEmpName { get; set; } = null!;

    public string NvqEmpLevel { get; set; } = null!;

    public DateOnly NvqEmpStartDate { get; set; }

    public bool NvqEmpStatus { get; set; }
}
