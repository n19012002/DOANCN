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
                            NgayCapnhat = DateTime.Now 
                        };

                        _context.TblChitietPhieuRls.Add(chitietPhieu);
                    }
                }

              
                var phieu = _context.TblPhieurenluyens.FirstOrDefault(p => p.IdphieuRl == idPhieu);
                if (phieu != null)
                {
                    phieu.Trangthaiphieu = true; 
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

    }
}
