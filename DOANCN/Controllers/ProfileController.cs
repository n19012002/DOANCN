using DOANCN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOANCN.Controllers
{
	public class ProfileController : Controller
	{
		private readonly RenluyenContext _context;

		public ProfileController(RenluyenContext context)
		{
			_context = context;

		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Profile()
		{
			if (HttpContext.Session.GetInt32("IsLoggedIn") == 1)
			{
				long? userID = HttpContext.Session.GetLong("ID");
				if (userID.HasValue)
				{

					var user = _context.TblSinhviens
				.Include(u => u.MachucvuNavigation)
				.Include(u => u.MaNganhNavigation)
					.ThenInclude(nganh => nganh.MaKhoaNavigation) 
				.Include(u => u.MatrangthaiNavigation)
				.Include(u => u.MaLopNavigation)
				.FirstOrDefault(u => u.Idsinhvien == userID);

					if (user != null)
					{
						return View(user);
					}
				}


			}

			return RedirectToAction("Index", "Login");

		}
	}
}
