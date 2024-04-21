using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblMucTieuChi
{
    public int IdmucTieuChi { get; set; }

    public string? Loai { get; set; }

    public int? Cha { get; set; }

    public int? Cap { get; set; }

    public string? Ten { get; set; }

    public int? ThangDiem { get; set; }

    public bool? ChoPhepNhap { get; set; }

    public bool? TinhTong { get; set; }

    public int? TongMax { get; set; }

    public bool? ChoPhepMinhChung { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgaySua { get; set; }

    public string? NguoiTao { get; set; }

    public string? NguoiSua { get; set; }

    public string? Mota { get; set; }

    public bool? TrangThaiMuc { get; set; }

    public virtual ICollection<TblChitietPhieuRl> TblChitietPhieuRls { get; set; } = new List<TblChitietPhieuRl>();
}
