using Microsoft.AspNetCore.Mvc;
using Medical_Inventory.Data.IRepository;
using Medical_Inventory.Models;
using Microsoft.AspNetCore.Authorization;

namespace Medical_Inventory.Controllers;

[Authorize]
public class CategoriesController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public CategoriesController(ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    // GET: Categories
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryRepository.GetAll()!;

        return View(categories);
    }

    // GET: Categories/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        var category = await _categoryRepository.GetFirstOrDefault(c => c.Id == id)!;

        if (category == null || id == null)
            return NotFound();

        return View(category);
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
        var existsCategory = await _categoryRepository.GetByName(category.Name)!;
        
        if (existsCategory is not null)
        {
            ModelState.AddModelError(string.Empty, category.Name + " already exists");
        }
        if (!ModelState.IsValid) return View(category);

        await _categoryRepository.Add(category);
        await _categoryRepository.Save();

        return RedirectToAction(nameof(Index));
    }

    // GET: Categories/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        var category = await _categoryRepository.GetFirstOrDefault(c => c.Id == id)!;

        if (category == null || id == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: Categories/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid) return View(category);

        _categoryRepository.Update(category);
        await _categoryRepository.Save();

        return RedirectToAction(nameof(Index));
    }

    // GET: Categories/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        var category = await _categoryRepository.GetFirstOrDefault(c => c.Id == id)!;

        if (category == null || id == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        _categoryRepository.Remove(id);
        await _categoryRepository.Save();

        return RedirectToAction(nameof(Index));
    }
}