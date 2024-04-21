using System;
using System.Collections.Generic;

namespace DOANCN.Models;

public partial class TblCanBo
{
    public int IdcanBo { get; set; }

    public long? Idsinhvien { get; set; }

    public string? MoTa { get; set; }

    public virtual TblSinhvien? IdsinhvienNavigation { get; set; }
}
