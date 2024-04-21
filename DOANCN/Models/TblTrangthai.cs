using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblTrangthai
{
    public int Matrangthai { get; set; }

    public string? Tentrangthai { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<TblSinhvien> TblSinhviens { get; set; } = new List<TblSinhvien>();
}
