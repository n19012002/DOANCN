using DOANCN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            TempData["IdPhieu"] = idPhieu;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Save(Dictionary<int, string> DiemTuDanhGia, Dictionary<int, string> DiemLopTruongDanhGia, int totalScore, IFormFileCollection minhChung, string mucTieuChiIds)
        {
            try
            {
                List<int?> mucTieuChiIdList = JsonConvert.DeserializeObject<List<int?>>(mucTieuChiIds);
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
                        await _context.SaveChangesAsync();
                        var idChitietPhieu = chitietPhieu.IdchitietPhieuRl;

                        var mucTieuChi = _context.TblChitietPhieuRls
                            .Include(m => m.IdmucTieuChiNavigation)
                            .FirstOrDefault(m => m.IdchitietPhieuRl == idChitietPhieu && m.IdmucTieuChiNavigation.ChoPhepMinhChung == true);

                        if (mucTieuChi != null && mucTieuChiIdList.Contains(mucTieuChi.IdmucTieuChi))
                        {
                            
                            var mucTieuChiFiles = Request.Form.Files.Where(f => f.Name.Contains($"minhChung[{mucTieuChi.IdmucTieuChi}]")).ToList();
                            var mucTieuChiFiles1 = minhChung.Where(f => f.Name.Contains($"minhChung[{mucTieuChi.IdmucTieuChi}]")).ToList();

                            foreach (var file in mucTieuChiFiles)
                            {
                                if (file != null && file.Length > 0)
                                {
                                    string msv = HttpContext.Session.GetString("MSV");

                                    if (string.IsNullOrEmpty(msv))
                                    {
                                        return BadRequest("Mã số sinh viên không hợp lệ.");
                                    }

                                    string minhChungFolder = Path.Combine("wwwroot", "minhchung", msv);
                                    Directory.CreateDirectory(minhChungFolder);

                                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                                    string filePath = Path.Combine(minhChungFolder, uniqueFileName);
                                    await using var stream = new FileStream(filePath, FileMode.Create);
                                    await file.CopyToAsync(stream);

                                    var minhChung1 = new TblMinhChung
                                    {
                                        IdchitietPhieuRl = idChitietPhieu,
                                        Link = filePath,
                                        NgayTao = DateTime.Now
                                    };

                                    _context.TblMinhChungs.Add(minhChung1);
                                }
                            }
                        }
                    }
                }

                
                var phieu = _context.TblPhieurenluyens.FirstOrDefault(p => p.IdphieuRl == idPhieu);
                if (phieu != null)
                {
                    phieu.Trangthaiphieu = true;
                    phieu.TongDiem = totalScore;
                }
                await _context.SaveChangesAsync();

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
