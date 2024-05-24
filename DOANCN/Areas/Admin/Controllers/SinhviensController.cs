using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DOANCN.Models;

namespace DOANCN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SinhviensController : Controller
    {
        private readonly RenluyenContext _context;

        public SinhviensController(RenluyenContext context)
        {
            _context = context;
        }

        // GET: Admin/Sinhviens
        public async Task<IActionResult> Index()
        {
            var renluyenContext = _context.TblSinhviens.Include(t => t.MaLopNavigation).Include(t => t.MaNganhNavigation).Include(t => t.MachucvuNavigation).Include(t => t.MatrangthaiNavigation);
            return View(await renluyenContext.ToListAsync());
        }

        // GET: Admin/Sinhviens/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSinhvien = await _context.TblSinhviens
                .Include(t => t.MaLopNavigation)
                .Include(t => t.MaNganhNavigation)
                .Include(t => t.MachucvuNavigation)
                .Include(t => t.MatrangthaiNavigation)
                .FirstOrDefaultAsync(m => m.Idsinhvien == id);
            if (tblSinhvien == null)
            {
                return NotFound();
            }

            return View(tblSinhvien);
        }

        // GET: Admin/Sinhviens/Create
        public IActionResult Create()
        {
            ViewData["MaLop"] = new SelectList(_context.TblLops, "MaLop", "TenLop");
            ViewData["MaNganh"] = new SelectList(_context.TblNganhs, "MaNganh", "TenNganh");
            ViewData["Machucvu"] = new SelectList(_context.TblChucvus, "Machucvu", "Tenchucvu");
            ViewData["Matrangthai"] = new SelectList(_context.TblTrangthais, "Matrangthai", "Tentrangthai");
            return View();
        }

        // POST: Admin/Sinhviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idsinhvien,Msv,Ho,TenDem,Ten,Sodienthoai,Diachi,Cccd,Gioitinh,NgayNhapHoc,MaNganh,Machucvu,MaLop,Matrangthai,MatKhau,AnhDaiDien")] TblSinhvien tblSinhvien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblSinhvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLop"] = new SelectList(_context.TblLops, "MaLop", "TenLop", tblSinhvien.MaLop);
            ViewData["MaNganh"] = new SelectList(_context.TblNganhs, "MaNganh", "TenNganh", tblSinhvien.MaNganh);
            ViewData["Machucvu"] = new SelectList(_context.TblChucvus, "Machucvu", "Tenchucvu", tblSinhvien.Machucvu);
            ViewData["Matrangthai"] = new SelectList(_context.TblTrangthais, "Matrangthai", "Tentrangthai", tblSinhvien.Matrangthai);
            return View(tblSinhvien);
        }

        // GET: Admin/Sinhviens/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSinhvien = await _context.TblSinhviens
               .Include(t => t.MaLopNavigation)
               .Include(t => t.MaNganhNavigation)
               .Include(t => t.MachucvuNavigation)
               .Include(t => t.MatrangthaiNavigation)
               .FirstOrDefaultAsync(m => m.Idsinhvien == id);
            if (tblSinhvien == null)
            {
                return NotFound();
            }
            ViewData["MaLop"] = new SelectList(_context.TblLops, "MaLop", "TenLop", tblSinhvien.MaLopNavigation.TenLop);
            ViewData["MaNganh"] = new SelectList(_context.TblNganhs, "MaNganh", "TenNganh", tblSinhvien.MaNganhNavigation.TenNganh);
            ViewData["Machucvu"] = new SelectList(_context.TblChucvus, "Machucvu", "Tenchucvu", tblSinhvien.MachucvuNavigation.Tenchucvu);
            ViewData["Matrangthai"] = new SelectList(_context.TblTrangthais, "Matrangthai", "Tentrangthai", tblSinhvien.MatrangthaiNavigation.Tentrangthai);
            return View(tblSinhvien);
        }

        // POST: Admin/Sinhviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Idsinhvien,Msv,Ho,TenDem,Ten,Sodienthoai,Diachi,Cccd,Gioitinh,NgayNhapHoc,MaNganh,Machucvu,MaLop,Matrangthai,MatKhau,AnhDaiDien")] TblSinhvien tblSinhvien)
        {
            if (id != tblSinhvien.Idsinhvien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblSinhvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblSinhvienExists(tblSinhvien.Idsinhvien))
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
            ViewData["MaLop"] = new SelectList(_context.TblLops, "MaLop", "MaLop", tblSinhvien.MaLop);
            ViewData["MaNganh"] = new SelectList(_context.TblNganhs, "MaNganh", "MaNganh", tblSinhvien.MaNganh);
            ViewData["Machucvu"] = new SelectList(_context.TblChucvus, "Machucvu", "Machucvu", tblSinhvien.Machucvu);
            ViewData["Matrangthai"] = new SelectList(_context.TblTrangthais, "Matrangthai", "Matrangthai", tblSinhvien.Matrangthai);
            return View(tblSinhvien);
        }

        // GET: Admin/Sinhviens/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSinhvien = await _context.TblSinhviens
                .Include(t => t.MaLopNavigation)
                .Include(t => t.MaNganhNavigation)
                .Include(t => t.MachucvuNavigation)
                .Include(t => t.MatrangthaiNavigation)
                .FirstOrDefaultAsync(m => m.Idsinhvien == id);
            if (tblSinhvien == null)
            {
                return NotFound();
            }

            return View(tblSinhvien);
        }

        // POST: Admin/Sinhviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblSinhvien = await _context.TblSinhviens.FindAsync(id);
            if (tblSinhvien != null)
            {
                _context.TblSinhviens.Remove(tblSinhvien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblSinhvienExists(long id)
        {
            return _context.TblSinhviens.Any(e => e.Idsinhvien == id);
        }
    }
}
