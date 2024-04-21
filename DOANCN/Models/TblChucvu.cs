using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblChucvu
{
    public int Machucvu { get; set; }

    public string? Tenchucvu { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<TblSinhvien> TblSinhviens { get; set; } = new List<TblSinhvien>();
}
