using DOANCN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOANCN.Controllers
{
    public class ChiTietDiemRLController : Controller
    {
        private readonly RenluyenContext _context;
        public ChiTietDiemRLController(RenluyenContext context)
        {
            _context = context;

        }
        // GET: MucTieuChi
        public async Task<IActionResult> Index()
        {
            var mucTieuChi = await _context.TblMucTieuChis
                .Where(u => u.TrangThaiMuc == true)
                .ToListAsync();
            return View(mucTieuChi);
        }

        [HttpPost]
        

        public IActionResult ChuyenDenNhapDiem(int idPhieu)
        {
            // Lưu idPhiếu vào TempData để sử dụng ở action nhập điểm
            TempData["IdPhieu"] = idPhieu;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Save(Dictionary<int, string> DiemTuDanhGia, Dictionary<int, string> DiemLopTruongDanhGia, int totalScore)
        {
            try
            {
                int? idPhieu = TempData["IdPhieu"] as int?;

                if (!idPhieu.HasValue)
                {
                    return BadRequest("Không tìm thấy idPhieu trong TempData.");
                }

                foreach (var kvp in DiemTuDanhGia)
                {
                    if (DiemLopTruongDanhGia.ContainsKey(kvp.Key))
                    {
                        var idmucTieuChi = kvp.Key;
                        var diemTuDanhGia = kvp.Value;
                        var diemLopTruongDanhGia = DiemLopTruongDanhGia[idmucTieuChi];

                        var chitietPhieu = new TblChitietPhieuRl
                        {
                            IdphieuRl = idPhieu.Value,
                            IdmucTieuChi = idmucTieuChi,
                            DiemTuCham = Convert.ToInt32(diemTuDanhGia),
                            DiemLopTruong = Convert.ToInt32(diemLopTruongDanhGia),
                            NgayCapnhat = DateTime.Now // Thêm ngày cập nhật (nếu cần)
                        };

                        _context.TblChitietPhieuRls.Add(chitietPhieu);
                    }
                }

                // Lấy phiếu từ cơ sở dữ liệu và cập nhật trạng thái
                var phieu = _context.TblPhieurenluyens.FirstOrDefault(p => p.IdphieuRl == idPhieu);
                if (phieu != null)
                {
                    phieu.Trangthaiphieu = true; // Cập nhật trạng thái thành đã hoàn thành
                    phieu.TongDiem = totalScore;
                }
                _context.SaveChanges();

                return RedirectToAction("Index", "ChamDiemRL");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }

        public IActionResult XemDiem(int idPhieu)
        {
            // Lấy thông tin chi tiết phiếu dựa trên idPhieu và bổ sung thông tin về mục tiêu chí
            var chiTietPhieu = _context.TblChitietPhieuRls
                .Include(c => c.IdmucTieuChiNavigation) // Bổ sung thông tin về mục tiêu chí
                .Where(c => c.IdphieuRl == idPhieu)
                .ToList();

            // Kiểm tra nếu không tìm thấy chi tiết phiếu, trả về trang không tìm thấy
            if (chiTietPhieu == null)
            {
                return NotFound();
            }

            var Phieu = _context.TblPhieurenluyens
                .Where(c => c.IdphieuRl == idPhieu)
                .ToList();
            // Lấy danh sách mục tiêu chí
            var mucTieuChi = _context.TblMucTieuChis.ToList();

            // Tạo một Tuple chứa cả hai danh sách chi tiết phiếu và mục tiêu chí
            var viewModel = new Tuple<List<TblChitietPhieuRl>, List<TblMucTieuChi>, List<TblPhieurenluyen>>(chiTietPhieu, mucTieuChi, Phieu);

            return View(viewModel);
        }

    }
}
