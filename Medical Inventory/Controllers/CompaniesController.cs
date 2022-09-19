using Medical_Inventory.Data;
using Microsoft.AspNetCore.Mvc;
using Medical_Inventory.Data.IRepository;
using Medical_Inventory.Models;
using Microsoft.AspNetCore.Authorization;
using Medical_Inventory.Data.IRepository.Repository;
using System.Data;
using Medical_Inventory.Exceptions;
using System.Security.Claims;
using Entities;
using Inventory.DAL;

namespace Medical_Inventory.Controllers;

[Authorize(Roles = StaticData.RoleAdmin)]
public class CompaniesController : Controller
{
    private readonly ILogger<CompaniesController> _logger;
    private readonly ICompanyRepository _companyRepository;

    public CompaniesController(ILogger<CompaniesController> logger, ICompanyRepository companyRepository)
    {
        _logger = logger;
        _companyRepository = companyRepository;
    }


    // GET: Companies
    public async Task<IActionResult> Index()
    {
        try
        {
            var result = await _companyRepository.GetAll()!;
            return View(result);
        }
        catch (Exception)
        {
            _logger.LogError("failed to load categories");
        }

        return View(null);
    }

    // GET: Companies/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        try
        {
            var result = await _companyRepository.GetFirstOrDefault(id)!;
            return View(result);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex.Message);
            return RedirectToAction("PageNotFound", "Home");
        }
        catch (Exception)
        {
            throw;
        }
        
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
            var existsCompany = await _companyRepository.GetByName(company.Name)!;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            company.CreatedTime = DateTime.Now;
            company.UpdatedTime = DateTime.Now;
            company.CreatedById = long.Parse(userId);
            company.UpdatedById = long.Parse(userId);

            await _companyRepository.Add(company);

            _logger.LogInformation(message: $"new company added with name of {company.Name}");
            return RedirectToAction(nameof(Index));
        }
        catch (DuplicationException ex)
        {
            ModelState.AddModelError(string.Empty, company.Name + " already exists");
            _logger.LogWarning(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            throw;
        }

        if (!ModelState.IsValid) return View(company);

        return RedirectToAction(nameof(Index));
    }

    // GET: Companies/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        try
        {
            var company = await _companyRepository.GetFirstOrDefault(id)!;

            return View(company);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning($"category not found of id: {id}");
            return RedirectToAction("PageNotFound", "Home");
        }
        catch (Exception)
        {

            throw;
        }

    }

    // POST: Companies/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Phone,Address")] Company company)
    {
        try
        {
            var existsCompany = await _companyRepository.GetByName(company.Name, id)!;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            company.UpdatedById = long.Parse(userId);

            _companyRepository.Update(company);
            await _companyRepository.Save();

            _logger.LogWarning($"category updated of id: {id}");

            return RedirectToAction(nameof(Index));
        }
        catch (NotFoundException)
        {
            _logger.LogWarning($"company not found of id: {id}");
            return NotFound();
        }
        catch (DuplicationException ex)
        {
            _logger.LogWarning($"company alaready exists of name: {company.Name}");
            ModelState.AddModelError(string.Empty, company.Name + " already exists");
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Failed to update data of id: {id}");
            _logger.LogWarning(ex.Message);
        }

        return View(company);
    }

    // GET: Companies/Delete/5
    public async Task<IActionResult> Delete(long? id)
    {
        try
        {
            var company = await _companyRepository.GetFirstOrDefault(id)!;
            if (company != null)
            {
                return View(company);
            }
        }
        catch (Exception)
        {
            _logger.LogError($"something wrong to find company with id: {id}");
        }
        
        return RedirectToAction("PageNotFound", "Home");
    }

    // POST: Companies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        try
        {
            var company = await _companyRepository.GetFirstOrDefault(id)!;
            if (company != null)
            {
                await _companyRepository.Remove(company);
            }
        }
        catch (Exception)
        {
            _logger.LogError($"something wrong to find company with id: {id}");
        }        
            
        return RedirectToAction(nameof(Index));
    }
        
}