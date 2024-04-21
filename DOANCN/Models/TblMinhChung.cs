using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblMinhChung
{
    public int IdminhChung { get; set; }

    public int? IdchitietPhieuRl { get; set; }

    public string? MoTa { get; set; }

    public string? Link { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayDuyet { get; set; }

    public virtual TblChitietPhieuRl? IdchitietPhieuRlNavigation { get; set; }
}
