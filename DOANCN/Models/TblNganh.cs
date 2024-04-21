using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblNganh
{
    public string MaNganh { get; set; } = null!;

    public int? MaKhoa { get; set; }

    public string? TenNganh { get; set; }

    public string? MoTa { get; set; }

    public virtual TblKhoa? MaKhoaNavigation { get; set; }

    public virtual ICollection<TblSinhvien> TblSinhviens { get; set; } = new List<TblSinhvien>();
}
