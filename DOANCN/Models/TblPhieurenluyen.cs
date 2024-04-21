using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblPhieurenluyen
{
    public int IdphieuRl { get; set; }

    public long? Idsinhvien { get; set; }

    public int? Idkyhoc { get; set; }

    public DateTime? Thoigiannap { get; set; }

    public string? Nguoicham { get; set; }

    public bool? Trangthaiphieu { get; set; }

    public string? Mota { get; set; }

    public int? TongDiem { get; set; }

    public int? DiemLopTruong { get; set; }

    public virtual TblHocKy? IdkyhocNavigation { get; set; }

    public virtual TblSinhvien? IdsinhvienNavigation { get; set; }

    public virtual ICollection<TblChitietPhieuRl> TblChitietPhieuRls { get; set; } = new List<TblChitietPhieuRl>();
}
