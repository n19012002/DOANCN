using DOANCN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOANCN.Controllers
{
    public class CanBoLoginController : Controller
    {
        private readonly RenluyenContext _context;
        public CanBoLoginController(RenluyenContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = _context.TblCanBos
        .Include(u => u.IdsinhvienNavigation)
         .ThenInclude(mtt => mtt.MatrangthaiNavigation)
        .Include(u => u.IdsinhvienNavigation)
         .ThenInclude(ml => ml.MaLopNavigation)
        .FirstOrDefault(u => u.IdsinhvienNavigation.Msv == username);

            if (user != null && password == user.IdsinhvienNavigation.MatKhau)
            {
                if (user.IdsinhvienNavigation.MatrangthaiNavigation.Matrangthai != 1)
                {
                    TempData["ErrorMessage"] = "Tài khoản bị khóa";
                }
                else
                {
                    HttpContext.Session.SetInt32("IsLogged", 1);

                    string fullName = string.Concat(user.IdsinhvienNavigation.Ho, " ", user.IdsinhvienNavigation.TenDem, " ", user.IdsinhvienNavigation.Ten);
                    HttpContext.Session.SetString("UserName", fullName);

                    // Lưu MaKhoa và MaLop vào session
                    HttpContext.Session.SetString("MaNganh", user.IdsinhvienNavigation.MaNganh);
                    HttpContext.Session.SetInt32("MaLop", user.IdsinhvienNavigation.MaLop ?? 0);
                    TempData["SuccessMessage"] = "Đăng nhập thành công!";
                }


            }
            else
            {
                TempData["ErrorMessage"] = "Tên người dùng hoặc mật khẩu không đúng";
            }


            return RedirectToAction("Login","CanBoLogin");
        }

        public IActionResult DanhSachSinhVien()
        {
            // Lấy MaNganh và MaLop từ session
            string maNganh = HttpContext.Session.GetString("MaNganh");
            int? maLop = HttpContext.Session.GetInt32("MaLop");

            // Truy vấn danh sách sinh viên thuộc ngành và lớp đã chọn
            var danhSachSinhVien = _context.TblSinhviens
                .Include(u => u.MaLopNavigation)
                .Include(u => u.MaNganhNavigation)
                .Where(sv => sv.MaNganh == maNganh && sv.MaLop == maLop)
                .ToList();

            return View(danhSachSinhVien);
        }
    }
}
