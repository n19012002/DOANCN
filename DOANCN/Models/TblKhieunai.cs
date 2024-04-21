using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblKhieunai
{
    public long Idkhieunai { get; set; }

    public long? Idsinhvien { get; set; }

    public DateTime? Thoigiankhieunai { get; set; }

    public string? Noidung { get; set; }

    public string? TrangThai { get; set; }

    public string? Nguoixuly { get; set; }

    public string? LoaiKhieunai { get; set; }

    public string? Mota { get; set; }

    public virtual TblSinhvien? IdsinhvienNavigation { get; set; }
}
