
using DOANCN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DOANCN.Controllers
{
    public class LoginController : Controller
    {
        private readonly RenluyenContext _context;
        public LoginController(RenluyenContext context)
        {
            _context = context;

        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = _context.TblSinhviens
        .Include(u => u.MachucvuNavigation)
        .Include(u => u.MaNganhNavigation)
        .Include(u => u.MatrangthaiNavigation)
        .FirstOrDefault(u => u.Msv == username);

            if (user != null && password == user.MatKhau && user.MachucvuNavigation.Machucvu == 1)
            {
                if (user.MatrangthaiNavigation.Matrangthai != 1)
                {
                    TempData["ErrorMessage"] = "Tài khoản bị khóa";
                }
                else
                {
                    HttpContext.Session.SetInt32("IsLoggedIn", 1);

                    string fullName = string.Concat(user.Ho, " ", user.TenDem, " ", user.Ten);
                    HttpContext.Session.SetString("UserName", fullName);
                    HttpContext.Session.SetLong("ID", user.Idsinhvien);
                    TempData["SuccessMessage"] = "Đăng nhập thành công!";
                }


            }
            else
            {
                TempData["ErrorMessage"] = "Tên người dùng hoặc mật khẩu không đúng";
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }



    }
}