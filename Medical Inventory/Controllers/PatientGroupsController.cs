using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Medical_Inventory.Data;
using Medical_Inventory.Models;

namespace Medical_Inventory.Controllers
{
    public class PatientGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PatientGroups
        public async Task<IActionResult> Index()
        {
              return _context.PatientGroup != null ? 
                          View(await _context.PatientGroup.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PatientGroup'  is null.");
        }

        // GET: PatientGroups/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.PatientGroup == null)
            {
                return NotFound();
            }

            var patientGroup = await _context.PatientGroup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientGroup == null)
            {
                return NotFound();
            }

            return View(patientGroup);
        }

        // GET: PatientGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatientGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PatientGroup patientGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patientGroup);
        }

        // GET: PatientGroups/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.PatientGroup == null)
            {
                return NotFound();
            }

            var patientGroup = await _context.PatientGroup.FindAsync(id);
            if (patientGroup == null)
            {
                return NotFound();
            }
            return View(patientGroup);
        }

        // POST: PatientGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] PatientGroup patientGroup)
        {
            if (id != patientGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientGroupExists(patientGroup.Id))
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
            return View(patientGroup);
        }

        // GET: PatientGroups/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.PatientGroup == null)
            {
                return NotFound();
            }

            var patientGroup = await _context.PatientGroup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientGroup == null)
            {
                return NotFound();
            }

            return View(patientGroup);
        }

        // POST: PatientGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.PatientGroup == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PatientGroup'  is null.");
            }
            var patientGroup = await _context.PatientGroup.FindAsync(id);
            if (patientGroup != null)
            {
                _context.PatientGroup.Remove(patientGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientGroupExists(long id)
        {
          return (_context.PatientGroup?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
