using Medical_Inventory.Data;
using Microsoft.AspNetCore.Mvc;
using Medical_Inventory.Data.IRepository;
using Medical_Inventory.Models;
using Microsoft.AspNetCore.Authorization;
using Medical_Inventory.Data.IRepository.Repository;
using System.Data;
using Medical_Inventory.Exceptions;

namespace Medical_Inventory.Controllers;

[Authorize(Roles = StaticData.RoleAdmin)]
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
        try
        {
            await _companyRepository.Add(company);
        }
        catch (DuplicationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message.ToString());
            //Console.WriteLine(ex.Message + " already exists");
        }
        catch (Exception ex)
        {
            Console.WriteLine("default");
            Console.WriteLine(ex.Message);
        }

        if (!ModelState.IsValid) return View(company);

       
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