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
    public class ChucvusController : Controller
    {
        private readonly RenluyenContext _context;

        public ChucvusController(RenluyenContext context)
        {
            _context = context;
        }

        // GET: Admin/Chucvus
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblChucvus.ToListAsync());
        }

        // GET: Admin/Chucvus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblChucvu = await _context.TblChucvus
                .FirstOrDefaultAsync(m => m.Machucvu == id);
            if (tblChucvu == null)
            {
                return NotFound();
            }

            return View(tblChucvu);
        }

        // GET: Admin/Chucvus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Chucvus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Machucvu,Tenchucvu,MoTa")] TblChucvu tblChucvu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblChucvu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblChucvu);
        }

        // GET: Admin/Chucvus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblChucvu = await _context.TblChucvus.FindAsync(id);
            if (tblChucvu == null)
            {
                return NotFound();
            }
            return View(tblChucvu);
        }

        // POST: Admin/Chucvus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Machucvu,Tenchucvu,MoTa")] TblChucvu tblChucvu)
        {
            if (id != tblChucvu.Machucvu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblChucvu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblChucvuExists(tblChucvu.Machucvu))
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
            return View(tblChucvu);
        }

        // GET: Admin/Chucvus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblChucvu = await _context.TblChucvus
                .FirstOrDefaultAsync(m => m.Machucvu == id);
            if (tblChucvu == null)
            {
                return NotFound();
            }

            return View(tblChucvu);
        }

        // POST: Admin/Chucvus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblChucvu = await _context.TblChucvus.FindAsync(id);
            if (tblChucvu != null)
            {
                _context.TblChucvus.Remove(tblChucvu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblChucvuExists(int id)
        {
            return _context.TblChucvus.Any(e => e.Machucvu == id);
        }
    }
}
