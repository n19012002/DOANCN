using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblSinhvien
{
    public long Idsinhvien { get; set; }

    public string Msv { get; set; } = null!;

    public string? Ho { get; set; }

    public string? TenDem { get; set; }

    public string? Ten { get; set; }

    public string? Sodienthoai { get; set; }

    public string? Diachi { get; set; }

    public string? Cccd { get; set; }

    public bool? Gioitinh { get; set; }

    public DateTime? NgayNhapHoc { get; set; }

    public string? MaNganh { get; set; }

    public int? Machucvu { get; set; }

    public int? MaLop { get; set; }

    public int? Matrangthai { get; set; }

    public string? MatKhau { get; set; }

    public string? AnhDaiDien { get; set; }

    public virtual TblLop? MaLopNavigation { get; set; }

    public virtual TblNganh? MaNganhNavigation { get; set; }

    public virtual TblChucvu? MachucvuNavigation { get; set; }

    public virtual TblTrangthai? MatrangthaiNavigation { get; set; }

    public virtual ICollection<TblCanBo> TblCanBos { get; set; } = new List<TblCanBo>();

    public virtual ICollection<TblDiemrenluyen> TblDiemrenluyens { get; set; } = new List<TblDiemrenluyen>();

    public virtual ICollection<TblKhieunai> TblKhieunais { get; set; } = new List<TblKhieunai>();

    public virtual ICollection<TblPhieurenluyen> TblPhieurenluyens { get; set; } = new List<TblPhieurenluyen>();
}
