using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblTroLySv
{
    public int IdtroLySv { get; set; }

    public string? TenTlsv { get; set; }

    public int? MaKhoa { get; set; }

    public virtual TblKhoa? MaKhoaNavigation { get; set; }
}
