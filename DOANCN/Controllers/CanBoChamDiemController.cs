using DOANCN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DOANCN.Controllers
{
    public class CanBoChamDiemController : Controller
    {
        private readonly RenluyenContext _context;

        public CanBoChamDiemController(RenluyenContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Lấy danh sách kì học từ cơ sở dữ liệu
            var danhSachKiHoc = _context.TblHocKies.Select(k => new SelectListItem
            {
                Value = k.Idkyhoc.ToString(),
                Text = $"{k.Tenhocky} - {k.NamHoc}"
            }).ToList();

            // Thêm một lựa chọn mặc định
            danhSachKiHoc.Insert(0, new SelectListItem("-- Chọn kì học --", ""));

            // Gửi danh sách kì học đến view để hiển thị trong dropdown
            ViewBag.DanhSachKiHoc = danhSachKiHoc;

            return View();
        }

        [HttpPost]
        [Route("CanBoChamDiem/HienThi")]
        public IActionResult HienThi(int kiHocId)
        {


            string maNganh = HttpContext.Session.GetString("MaNganh");
            int? maLop = HttpContext.Session.GetInt32("MaLop");

            string tenLop = "";

            var lop = _context.TblLops.FirstOrDefault(l => l.MaLop == maLop);
            if (lop != null)
            {
                tenLop = lop.TenLop;
            }

            var danhSachSinhVien = _context.TblSinhviens
                .Include(u => u.MaLopNavigation)
                .Include(u => u.MaNganhNavigation)
                .ThenInclude(mn => mn.MaKhoaNavigation)
                .Where(sv => sv.MaNganh == maNganh && sv.MaLop == maLop)
                .OrderBy(sv => sv.Ten)
                .ToList();


            ViewBag.TenLop = tenLop;

            var statusDict = new Dictionary<long, StudentStatus>();

            foreach (var sinhVien in danhSachSinhVien)
            {
                var phieu = _context.TblPhieurenluyens.FirstOrDefault(p => p.Idkyhoc == kiHocId && p.Idsinhvien == sinhVien.Idsinhvien);
                if (phieu != null)
                {
                    if (phieu.DiemLopTruong == 0)
                    {
                        statusDict[sinhVien.Idsinhvien] = new StudentStatus { Status = "Cán Bộ Chưa chấm", PhieuId = phieu.IdphieuRl };
                    }
                    else
                    {
                        statusDict[sinhVien.Idsinhvien] = new StudentStatus { Status = "Đã chấm", PhieuId = phieu.IdphieuRl };
                    }
                }
                else
                {
                    statusDict[sinhVien.Idsinhvien] = new StudentStatus { Status = "Sinh viên chưa tự chấm", PhieuId = null };
                }
            }


            // Gửi danh sách sinh viên và trạng thái của từng sinh viên đến view để hiển thị
            ViewBag.DanhSachSinhVien = danhSachSinhVien;
            ViewBag.StatusDict = statusDict;

            return View("DanhSachSinhVien");
        }

        public IActionResult XemDiem(int idPhieu)
        {

            var chiTietPhieu = _context.TblChitietPhieuRls
                .Include(c => c.IdmucTieuChiNavigation)
                .Where(c => c.IdphieuRl == idPhieu)
                .ToList();


            if (chiTietPhieu == null)
            {
                return NotFound();
            }

            var Phieu = _context.TblPhieurenluyens
                .Where(c => c.IdphieuRl == idPhieu)
                .ToList();

            var mucTieuChi = _context.TblMucTieuChis.ToList();


            var viewModel = new Tuple<List<TblChitietPhieuRl>, List<TblMucTieuChi>, List<TblPhieurenluyen>>(chiTietPhieu, mucTieuChi, Phieu);

            return View(viewModel);
        }

        public IActionResult ChamDiem(int idPhieu)
        {
            ViewBag.IdPhieu = idPhieu;
            var chiTietPhieu = _context.TblChitietPhieuRls
                .Include(c => c.IdmucTieuChiNavigation)
                .Where(c => c.IdphieuRl == idPhieu)
                .ToList();


            if (chiTietPhieu == null)
            {
                return NotFound();
            }

            var Phieu = _context.TblPhieurenluyens
                .Where(c => c.IdphieuRl == idPhieu)
                .ToList();

            var mucTieuChi = _context.TblMucTieuChis.ToList();


            var viewModel = new Tuple<List<TblChitietPhieuRl>, List<TblMucTieuChi>, List<TblPhieurenluyen>>(chiTietPhieu, mucTieuChi, Phieu);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Save([FromForm] Dictionary<int, Dictionary<int, int>> data)
        {
            try
            {
                if (data != null)
                {
                    foreach (var entry in data)
                    {
                        var idPhieu = entry.Key;
                        var diemList = entry.Value;

                        foreach (var idMucTieuChi in diemList.Keys)
                        {
                            var diemLopTruong = diemList[idMucTieuChi];

                            var chiTietPhieu = _context.TblChitietPhieuRls.FirstOrDefault(c => c.IdphieuRl == idPhieu && c.IdmucTieuChi == idMucTieuChi);
                            if (chiTietPhieu != null)
                            {
                                chiTietPhieu.DiemLopTruong = diemLopTruong;
                            }
                            else
                            {
                                // Xử lý khi không tìm thấy chi tiết phiếu
                            }
                        }
                    }

                    _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                    return Ok(new { message = "Dữ liệu đã được lưu." }); // Trả về thông báo khi lưu thành công
                }
                else
                {
                    return BadRequest(); // Trả về 400 Bad Request nếu dữ liệu không hợp lệ
                }
            }
            catch (Exception ex)
            {
                // Gửi lỗi dưới dạng phản hồi JSON
                return BadRequest(new { errorMessage = "Đã xảy ra lỗi khi lưu điểm lớp trưởng: " + ex.Message });
            }
        }


    }
}
