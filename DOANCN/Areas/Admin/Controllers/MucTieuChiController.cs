using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DOANCN.Models;
using X.PagedList;

namespace DOANCN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MucTieuChiController : Controller
    {
        private readonly RenluyenContext _context;

        public MucTieuChiController(RenluyenContext context)
        {
            _context = context;
        }

        // GET: Admin/MucTieuChi
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10; 
            int pageNumber = (page ?? 1); 

            var tblMucTieuChis = await _context.TblMucTieuChis.ToPagedListAsync(pageNumber, pageSize);

            return View(tblMucTieuChis);
        }

        // GET: Admin/MucTieuChi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMucTieuChi = await _context.TblMucTieuChis
                .FirstOrDefaultAsync(m => m.IdmucTieuChi == id);
            if (tblMucTieuChi == null)
            {
                return NotFound();
            }

            return View(tblMucTieuChi);
        }

        // GET: Admin/MucTieuChi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MucTieuChi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdmucTieuChi,Loai,Cha,Cap,Ten,ThangDiem,ChoPhepNhap,TinhTong,TongMax,ChoPhepMinhChung,NgayTao,NgaySua,NguoiTao,NguoiSua,Mota,TrangThaiMuc")] TblMucTieuChi tblMucTieuChi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblMucTieuChi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblMucTieuChi);
        }

        // GET: Admin/MucTieuChi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMucTieuChi = await _context.TblMucTieuChis.FindAsync(id);
            if (tblMucTieuChi == null)
            {
                return NotFound();
            }
            return View(tblMucTieuChi);
        }

        // POST: Admin/MucTieuChi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdmucTieuChi,Loai,Cha,Cap,Ten,ThangDiem,ChoPhepNhap,TinhTong,TongMax,ChoPhepMinhChung,NgayTao,NgaySua,NguoiTao,NguoiSua,Mota,TrangThaiMuc")] TblMucTieuChi tblMucTieuChi)
        {
            if (id != tblMucTieuChi.IdmucTieuChi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblMucTieuChi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblMucTieuChiExists(tblMucTieuChi.IdmucTieuChi))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblMucTieuChi);
        }

        // GET: Admin/MucTieuChi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMucTieuChi = await _context.TblMucTieuChis
                .FirstOrDefaultAsync(m => m.IdmucTieuChi == id);
            if (tblMucTieuChi == null)
            {
                return NotFound();
            }

            return View(tblMucTieuChi);
        }

        // POST: Admin/MucTieuChi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblMucTieuChi = await _context.TblMucTieuChis.FindAsync(id);
            if (tblMucTieuChi != null)
            {
                _context.TblMucTieuChis.Remove(tblMucTieuChi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblMucTieuChiExists(int id)
        {
            return _context.TblMucTieuChis.Any(e => e.IdmucTieuChi == id);
        }
    }
}
