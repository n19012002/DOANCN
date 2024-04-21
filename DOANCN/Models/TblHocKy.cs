using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblHocKy
{
    public int Idkyhoc { get; set; }

    public string? Tenhocky { get; set; }

    public int? NamHoc { get; set; }

    public string? Mota { get; set; }

    public virtual ICollection<TblDiemrenluyen> TblDiemrenluyens { get; set; } = new List<TblDiemrenluyen>();

    public virtual ICollection<TblPhieurenluyen> TblPhieurenluyens { get; set; } = new List<TblPhieurenluyen>();
}
