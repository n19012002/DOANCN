using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblLop
{
    public int MaLop { get; set; }

    public string? TenLop { get; set; }

    public int? MaNganh { get; set; }

    public virtual ICollection<TblSinhvien> TblSinhviens { get; set; } = new List<TblSinhvien>();
}
