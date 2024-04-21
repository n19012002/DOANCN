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
    public class NganhsController : Controller
    {
        private readonly RenluyenContext _context;

        public NganhsController(RenluyenContext context)
        {
            _context = context;
        }

        // GET: Admin/Nganhs
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblNganhs.ToListAsync());
        }

        // GET: Admin/Nganhs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblNganh = await _context.TblNganhs
                .FirstOrDefaultAsync(m => m.MaNganh == id);
            if (tblNganh == null)
            {
                return NotFound();
            }

            return View(tblNganh);
        }

        // GET: Admin/Nganhs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Nganhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNganh,TenNganh,MoTa")] TblNganh tblNganh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblNganh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblNganh);
        }

        // GET: Admin/Nganhs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblNganh = await _context.TblNganhs.FindAsync(id);
            if (tblNganh == null)
            {
                return NotFound();
            }
            return View(tblNganh);
        }

        // POST: Admin/Nganhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaNganh,TenNganh,MoTa")] TblNganh tblNganh)
        {
            if (id != tblNganh.MaNganh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblNganh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblNganhExists(tblNganh.MaNganh))
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
            return View(tblNganh);
        }

        // GET: Admin/Nganhs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblNganh = await _context.TblNganhs
                .FirstOrDefaultAsync(m => m.MaNganh == id);
            if (tblNganh == null)
            {
                return NotFound();
            }

            return View(tblNganh);
        }

        // POST: Admin/Nganhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tblNganh = await _context.TblNganhs.FindAsync(id);
            if (tblNganh != null)
            {
                _context.TblNganhs.Remove(tblNganh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblNganhExists(string id)
        {
            return _context.TblNganhs.Any(e => e.MaNganh == id);
        }
    }
}
