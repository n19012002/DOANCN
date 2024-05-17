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
    public class HocKiesController : Controller
    {
        private readonly RenluyenContext _context;

        public HocKiesController(RenluyenContext context)
        {
            _context = context;
        }

        // GET: Admin/HocKies
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblHocKies.ToListAsync());
        }

        // GET: Admin/HocKies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblHocKy = await _context.TblHocKies
                .FirstOrDefaultAsync(m => m.Idkyhoc == id);
            if (tblHocKy == null)
            {
                return NotFound();
            }

            return View(tblHocKy);
        }

        // GET: Admin/HocKies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/HocKies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idkyhoc,Tenhocky,NamHoc,Mota")] TblHocKy tblHocKy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblHocKy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblHocKy);
        }

        // GET: Admin/HocKies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblHocKy = await _context.TblHocKies.FindAsync(id);
            if (tblHocKy == null)
            {
                return NotFound();
            }
            return View(tblHocKy);
        }

        // POST: Admin/HocKies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idkyhoc,Tenhocky,NamHoc,Mota")] TblHocKy tblHocKy)
        {
            if (id != tblHocKy.Idkyhoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblHocKy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblHocKyExists(tblHocKy.Idkyhoc))
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
            return View(tblHocKy);
        }

        // GET: Admin/HocKies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblHocKy = await _context.TblHocKies
                .FirstOrDefaultAsync(m => m.Idkyhoc == id);
            if (tblHocKy == null)
            {
                return NotFound();
            }

            return View(tblHocKy);
        }

        // POST: Admin/HocKies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblHocKy = await _context.TblHocKies.FindAsync(id);
            if (tblHocKy != null)
            {
                _context.TblHocKies.Remove(tblHocKy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblHocKyExists(int id)
        {
            return _context.TblHocKies.Any(e => e.Idkyhoc == id);
        }
    }
}
