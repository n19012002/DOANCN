using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblKhoa
{
    public int MaKhoa { get; set; }

    public string? TenKhoa { get; set; }

    public virtual ICollection<TblNganh> TblNganhs { get; set; } = new List<TblNganh>();

    public virtual ICollection<TblTroLySv> TblTroLySvs { get; set; } = new List<TblTroLySv>();
}
