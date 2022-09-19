using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Security.Claims;
using Entities;
using Inventory.Service.IRepository;
using Inventory.Utility.Exceptions;

namespace Medical_Inventory.Controllers;

[Authorize]
public class GenericsController : Controller
{
    private readonly ILogger<CompaniesController> _logger;
    private readonly IGenericRepository _genericRepository;

    public GenericsController(ILogger<CompaniesController> logger, IGenericRepository genericRepository)
    {
        _logger = logger;
        _genericRepository = genericRepository;
    }

    // GET: Generics
    public async Task<IActionResult> Index()
    {
        try
        {
            var result = await _genericRepository.GetAll()!;

            return View(result);
        }
        catch (Exception)
        {
            _logger.LogError("failed to load generics");
        }

        return View(null);
    }

    // GET: Generics/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        try
        {
            var result = await _genericRepository.GetFirstOrDefault(id)!;
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
        try
        {
            var existsGeneric = await _genericRepository.GetByName(generic.Name)!;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            generic.CreatedTime = DateTime.Now;
            generic.UpdatedTime = DateTime.Now;
            generic.CreatedById = long.Parse(userId);
            generic.UpdatedById = long.Parse(userId);

            await _genericRepository.Add(generic);
            await _genericRepository.Save();

            _logger.LogInformation(message: $"new generic added with name of {generic.Name}");
            return RedirectToAction(nameof(Index));
        }
        catch (DuplicationException ex)
        {
            ModelState.AddModelError(string.Empty, generic.Name + " already exists");
            _logger.LogWarning(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            throw;
        }

        if (!ModelState.IsValid) return View(generic);

        return RedirectToAction(nameof(Index));
    }

    // GET: Generics/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        try
        {
            var generic = await _genericRepository.GetFirstOrDefault(id)!;

            return View(generic);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning($"generic not found of id: {id}");
            return RedirectToAction("PageNotFound", "Home");
        }
        catch (Exception)
        {
            throw;
        }
    }

    // POST: Generics/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Generic generic)
    {
        try
        {
            var existsCompany = await _genericRepository.GetByName(generic.Name, id)!;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            generic.UpdatedById = long.Parse(userId);

            _genericRepository.Update(generic);
            await _genericRepository.Save();

            _logger.LogWarning($"generic updated of id: {id}");

            return RedirectToAction(nameof(Index));
        }
        catch (NotFoundException)
        {
            _logger.LogWarning($"generic not found of id: {id}");
            return NotFound();
        }
        catch (DuplicationException ex)
        {
            _logger.LogWarning($"generic alaready exists of name: {generic.Name}");
            ModelState.AddModelError(string.Empty, generic.Name + " already exists");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to update generic of id: {id}");
            _logger.LogWarning(ex.Message);
        }

        return View(generic);
    }

    // GET: Generics/Delete/5
    public async Task<IActionResult> Delete(long? id)
    {
        var generic = await _genericRepository.GetFirstOrDefault(id)!;
        if (generic == null)
        {
            return NotFound();
        }

        return View(generic);
    }

    // POST: Generics/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        try
        {
            var generic = await _genericRepository.GetFirstOrDefault(id)!;
            if (generic != null)
            {
                await _genericRepository.Remove(generic);
            }
        }
        catch(Exception ex)
        {
            _logger.LogError($"failed to delete generic of id: {id}");
            _logger.LogError(ex.Message);
        }
        
        return RedirectToAction(nameof(Index));
    }
}