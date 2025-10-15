using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenVanQuan_2310900084.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NguyenVanQuan_2310900084.Controllers
{
    public class NvqEmployeesController : Controller
    {
        private readonly NguyenVanQuan2310900084Context _context;

        public NvqEmployeesController(NguyenVanQuan2310900084Context context)
        {
            _context = context;
        }

        // GET: NvqEmployees
        public async Task<IActionResult> NvqIndex()
        {
            return View(await _context.NvqEmployees.ToListAsync());
        }

        // GET: NvqEmployees/Details/5
        public async Task<IActionResult> NvqDetails(int? nvqId)
        {
            if (nvqId == null)
            {
                return NotFound();
            }

            var nvqEmployee = await _context.NvqEmployees
                .FirstOrDefaultAsync(m => m.NvqEmpId == nvqId);
            if (nvqEmployee == null)
            {
                return NotFound();
            }

            return View(nvqEmployee);
        }

        // GET: NvqEmployees/Create
        public IActionResult NvqCreate()
        {
            return View();
        }

        // POST: NvqEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NvqCreate([Bind("NvqEmpId,NvqEmpName,NvqEmpLevel,NvqEmpStartDate,NvqEmpStatus")] NvqEmployee nvqEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nvqEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(NvqIndex));
            }
            return View(nvqEmployee);
        }

        // GET: NvqEmployees/Edit/5
        public async Task<IActionResult> NvqEdit(int? nvqId)
        {
            if (nvqId == null)
            {
                return NotFound();
            }

            var nvqEmployee = await _context.NvqEmployees.FindAsync(nvqId);
            if (nvqEmployee == null)
            {
                return NotFound();
            }
            return View(nvqEmployee);
        }

        // POST: NvqEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int nvqId, [Bind("NvqEmpId,NvqEmpName,NvqEmpLevel,NvqEmpStartDate,NvqEmpStatus")] NvqEmployee nvqEmployee)
        {
            if (nvqId != nvqEmployee.NvqEmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nvqEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NvqEmployeeExists(nvqEmployee.NvqEmpId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(NvqIndex));
            }
            return View(nvqEmployee);
        }

        // GET: NvqEmployees/Delete/5
        public async Task<IActionResult> NvqDelete(int? nvqId)
        {
            if (nvqId == null)
            {
                return NotFound();
            }

            var nvqEmployee = await _context.NvqEmployees
                .FirstOrDefaultAsync(m => m.NvqEmpId == nvqId);
            if (nvqEmployee == null)
            {
                return NotFound();
            }

            return View(nvqEmployee);
        }

        // POST: NvqEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NvqDeleteConfirmed(int nvqId)
        {
            var nvqEmployee = await _context.NvqEmployees.FindAsync(nvqId);
            if (nvqEmployee != null)
            {
                _context.NvqEmployees.Remove(nvqEmployee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(NvqIndex));
        }

        private bool NvqEmployeeExists(int nvqId)
        {
            return _context.NvqEmployees.Any(e => e.NvqEmpId == nvqId);
        }
    }
}
