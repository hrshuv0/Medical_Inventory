using Microsoft.AspNetCore.Mvc;
using Medical_Inventory.Data.IRepository;
using Medical_Inventory.Models;

namespace Medical_Inventory.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }


        // GET: Companies
        public async Task<IActionResult> Index()
        {
            var result = await _companyRepository.GetAll()!;

            return View(result);
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var result = await _companyRepository.GetFirstOrDefault(id)!;

            if (result is null) return NotFound();

            return View(result);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Address")] Company company)
        {
            if (ModelState.IsValid)
            {
                if (!ModelState.IsValid) return View(company);

                await _companyRepository.Add(company);
            
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var company = await _companyRepository.GetFirstOrDefault(id)!;
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,Address")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _companyRepository.Update(company);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var company = await _companyRepository.GetFirstOrDefault(id)!;
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _companyRepository.GetFirstOrDefault(id)!;
            if (company != null)
            {
                await _companyRepository.Remove(company);
            }
            
            return RedirectToAction(nameof(Index));
        }
        
    }
}
