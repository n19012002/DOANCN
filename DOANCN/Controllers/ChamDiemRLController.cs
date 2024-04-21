using DOANCN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOANCN.Controllers
{
    public class ChamDiemRLController : Controller
    {
        private readonly RenluyenContext _context;
        public ChamDiemRLController(RenluyenContext context)
        {
            _context = context;

        }
        // GET: ChamDiemRL/Index
        public IActionResult Index()
        {
            var semesters = _context.TblHocKies.ToList();
            return View(semesters);
        }

        [HttpPost]
        public IActionResult GradeSheet(int selectedSemesterId)
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            long? userID = HttpContext.Session.GetLong("ID");
            if (!userID.HasValue)
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("Login", "Account");
            }

            // Kiểm tra xem đã tồn tại một phiếu chấm điểm cho người dùng và học kỳ đã chọn hay chưa
            var existingGradeSheet = _context.TblPhieurenluyens.FirstOrDefault(g => g.Idsinhvien == userID && g.Idkyhoc == selectedSemesterId);
            if (existingGradeSheet != null)
            {
                // Nếu đã tồn tại phiếu chấm điểm, không cho tạo mới và chuyển hướng đến trang chính
                return RedirectToAction("ChamDiemRL", "Index");
            }

            // Nếu chưa có phiếu, tạo một phiếu mới
            var newGradeSheet = new TblPhieurenluyen
            {
                Idsinhvien = userID,
                Idkyhoc = selectedSemesterId,
                Thoigiannap = DateTime.Now,
                Nguoicham = "Tên người chấm",
                Trangthaiphieu = false,
                Mota = "Mô tả chi tiết về phiếu",
                TongDiem = 0,
                DiemLopTruong = 0

            };

            // Lưu phiếu mới vào cơ sở dữ liệu
            _context.TblPhieurenluyens.Add(newGradeSheet);
            _context.SaveChanges();

            // Trả về kết quả JSON
            return Json(new { success = true, message = "Tạo phiếu thành công" });
        }

        // Action để kiểm tra và trả về thông tin phiếu đã tạo (nếu có)
        [HttpGet]
        public IActionResult CheckGradeSheet(int selectedSemesterId)
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            // Kiểm tra xem người dùng đã đăng nhập chưa
            long? userID = HttpContext.Session.GetLong("ID");
            if (!userID.HasValue)
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("Login", "Account");
            }

            // Kiểm tra xem đã có phiếu chấm điểm cho học kỳ hiện tại của người dùng chưa

            var existingGradeSheet = _context.TblPhieurenluyens
                .Include(g => g.IdsinhvienNavigation)
                .Include(g => g.IdkyhocNavigation)
                .FirstOrDefault(g => g.Idkyhoc == selectedSemesterId && g.Idsinhvien == userID);

            if (existingGradeSheet != null)
            {
                TempData["IDPhieu"] = existingGradeSheet.IdphieuRl;
                // Nếu đã có phiếu, trả về partial view chứa thông tin của phiếu
                return PartialView("_GradeSheetPartialView", existingGradeSheet);
            }

            // Nếu chưa có phiếu, trả về null
            return Json(new { message = "Không tìm thấy phiếu chấm điểm." });
        }

    }

}

