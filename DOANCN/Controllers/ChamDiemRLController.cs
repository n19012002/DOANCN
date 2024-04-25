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
      
            long? userID = HttpContext.Session.GetLong("ID");
            if (!userID.HasValue)
            {
              
                return RedirectToAction("Login", "Account");
            }

           
            var existingGradeSheet = _context.TblPhieurenluyens.FirstOrDefault(g => g.Idsinhvien == userID && g.Idkyhoc == selectedSemesterId);
            if (existingGradeSheet != null)
            {
             
                return RedirectToAction("ChamDiemRL", "Index");
            }

           
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

        
            _context.TblPhieurenluyens.Add(newGradeSheet);
            _context.SaveChanges();

           
            return Json(new { success = true, message = "Tạo phiếu thành công" });
        }

        
        [HttpGet]
        public IActionResult CheckGradeSheet(int selectedSemesterId)
        {

            long? userID = HttpContext.Session.GetLong("ID");
            if (!userID.HasValue)
            {
            
                return RedirectToAction("Login", "Account");
            }

          

            var existingGradeSheet = _context.TblPhieurenluyens
                .Include(g => g.IdsinhvienNavigation)
                .Include(g => g.IdkyhocNavigation)
                .FirstOrDefault(g => g.Idkyhoc == selectedSemesterId && g.Idsinhvien == userID);

            if (existingGradeSheet != null)
            {
                TempData["IDPhieu"] = existingGradeSheet.IdphieuRl;
                
                return PartialView("_GradeSheetPartialView", existingGradeSheet);
            }

       
            return Json(new { message = "Không tìm thấy phiếu chấm điểm." });
        }

    }

}

