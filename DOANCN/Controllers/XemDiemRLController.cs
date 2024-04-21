using DOANCN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOANCN.Controllers
{
	public class XemDiemRLController : Controller
	{
		private readonly RenluyenContext _context;
		public XemDiemRLController(RenluyenContext context)
		{
			_context = context;

		}
		public IActionResult Index()
		{
			long? userID = HttpContext.Session.GetLong("ID");
			var diemRenLuyenList = _context.TblDiemrenluyens
				.Include(u => u.IdsinhvienNavigation)
				.Include(u => u.IdkyhocNavigation)
				.Where(d => d.Idsinhvien == userID)
				.ToList();

            // Tính điểm trung bình của cả năm học
            var diemNamHoc = diemRenLuyenList
                .GroupBy(d => new { d.IdkyhocNavigation.NamHoc })
                .Select(g => new
                {
                    NamHoc = g.Key.NamHoc,
                    DiemTrungBinh = g.Average(d => (double)(d.DiemRl ?? 0))
                });

            ViewBag.DiemNamHoc = diemNamHoc;

            return View(diemRenLuyenList);

		}
	}
}
