using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblDiemrenluyen
{
    public long Idsinhvien { get; set; }

    public int Idkyhoc { get; set; }

    public int? DiemRl { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public string? MoTa { get; set; }

    public virtual TblHocKy IdkyhocNavigation { get; set; } = null!;

    public virtual TblSinhvien IdsinhvienNavigation { get; set; } = null!;
}
