using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Medical_Inventory.Data;
using Medical_Inventory.Data.IRepository;
using Medical_Inventory.Models;

namespace Medical_Inventory.Controllers
{
    public class GenericsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericRepository _genericRepository;

        public GenericsController(ApplicationDbContext context, IGenericRepository genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
        }

        // GET: Generics
        public async Task<IActionResult> Index()
        {
            var result = await _genericRepository.GetAll()!;

            return View(result);
        }

        // GET: Generics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var result = await _genericRepository.GetFirstOrDefault(id)!;

            if (result is null) return NotFound();

            return View(result);
        }

        // GET: Generics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Generics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Generic generic)
        {
            if (!ModelState.IsValid) return View(generic);

            await _genericRepository.Add(generic);
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Generics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Generic == null)
            {
                return NotFound();
            }

            var generic = await _context.Generic.FindAsync(id);
            if (generic == null)
            {
                return NotFound();
            }
            return View(generic);
        }

        // POST: Generics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Generic generic)
        {
            if (id != generic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _genericRepository.Update(generic);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(generic);
        }

        // GET: Generics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Generic == null)
            {
                return NotFound();
            }

            var generic = await _context.Generic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generic == null)
            {
                return NotFound();
            }

            return View(generic);
        }

        // POST: Generics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var generic = await _genericRepository.GetFirstOrDefault(id)!;
            if (generic != null)
            {
                await _genericRepository.Remove(generic);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
