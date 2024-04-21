using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblChitietPhieuRl
{
    public int IdchitietPhieuRl { get; set; }

    public int? IdphieuRl { get; set; }

    public int? IdmucTieuChi { get; set; }

    public int? DiemTuCham { get; set; }

    public int? DiemLopTruong { get; set; }

    public int? IdminhChung { get; set; }

    public DateTime? NgayCapnhat { get; set; }

    public virtual TblMucTieuChi? IdmucTieuChiNavigation { get; set; }

    public virtual TblPhieurenluyen? IdphieuRlNavigation { get; set; }

    public virtual ICollection<TblMinhChung> TblMinhChungs { get; set; } = new List<TblMinhChung>();
}
