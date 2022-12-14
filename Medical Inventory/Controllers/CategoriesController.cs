using System.Security.Claims;
using Entities;
using Inventory.DAL;
using Inventory.Service.IRepository;
using Inventory.Utility.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Inventory.Controllers;

[Authorize(Roles = StaticData.RoleAdmin)]
public class CategoriesController : Controller
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public CategoriesController(ILogger<CategoriesController> logger, ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _logger = logger;
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    // GET: Categories
    public async Task<IActionResult> Index()
    {
        try
        {
            var categories = await _categoryRepository.GetAll()!;
            return View(categories);
        }
        catch (Exception)
        {
            _logger.LogError("failed to load categories");
        }        

        return View(null);
    }

    // GET: Categories/Details/5
    public async Task<IActionResult> Details(long id)
    {
        try
        {
            var category = await _categoryRepository.GetFirstOrDefault(id)!;
            return View(category);
        }
        catch(NotFoundException ex)
        {
            _logger.LogWarning(ex.Message);
            return RedirectToAction("PageNotFound", "Home");
        }
        catch (Exception)
        {
            _logger.LogWarning($"category not found with id: {id}");
        }
        return RedirectToAction("PageNotFound", "Home");
    }

    // GET: Categories/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categories/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
    {
        try
        {
            var existsCategory = await _categoryRepository.GetByName(category.Name)!;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            category.CreatedTime = DateTime.Now;
            category.UpdatedTime = DateTime.Now;
            category.CreatedById = long.Parse(userId);
            category.UpdatedById = long.Parse(userId);

            await _categoryRepository.Add(category);
            await _categoryRepository.Save();

            _logger.LogInformation(message: $"new category added with name of {category.Name}");

            return RedirectToAction(nameof(Index));
        }
        catch(DuplicationException ex)
        {
            ModelState.AddModelError(string.Empty, category.Name + " already exists");
            _logger.LogWarning(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }

        if (!ModelState.IsValid) return View(category);        

        return RedirectToAction(nameof(Index));
    }

    // GET: Categories/Edit/5
    public async Task<IActionResult> Edit(long id)
    {
        try
        {
            var category = await _categoryRepository.GetFirstOrDefault(id)!;

            return View(category);
        }
        catch(NotFoundException ex)
        {
            _logger.LogWarning($"category not found of id: {id}\n" + ex);            
        }
        catch (Exception)
        {

        }
        return RedirectToAction("PageNotFound", "Home");
    }

    // POST: Categories/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
    {
        try
        {
            var existsCategory = await _categoryRepository.GetByName(category.Name, id)!;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            category.UpdatedById = long.Parse(userId);

            _categoryRepository.Update(category);
            await _categoryRepository.Save();

            _logger.LogWarning($"category updated of id: {id}");

            return RedirectToAction(nameof(Index));
        }
        catch(NotFoundException)
        {
            _logger.LogWarning($"category not found of id: {id}");
            return NotFound();
        }
        catch (DuplicationException ex)
        {
            _logger.LogWarning($"category alaready exists of name: {category.Name}\n" + ex);
            ModelState.AddModelError(string.Empty, category.Name + " already exists");
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Failed to update data of id: {id}");
            _logger.LogWarning(ex.Message);
        }

        return View(category);
    }

    // GET: Categories/Delete/5
    public async Task<IActionResult> Delete(long id)
    {
        var category = await _categoryRepository.GetFirstOrDefault(id)!;

        if (category == null)
        {
            return RedirectToAction("PageNotFound", "Home");
        }

        return View(category);
    }

    // POST: Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            _categoryRepository.Remove(id);
            await _categoryRepository.Save();

            _logger.LogWarning($"Deleted data of id: {id}");
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Failed to delete data of id: {id}\n" + ex.Message);
        }        

        return RedirectToAction(nameof(Index));
    }
}