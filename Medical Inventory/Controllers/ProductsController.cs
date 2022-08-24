using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Medical_Inventory.Data;
using Medical_Inventory.Data.IRepository;
using Medical_Inventory.Models;
using Medical_Inventory.Models.ViewModel;

namespace Medical_Inventory.Controllers;

public class ProductsController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IGenericRepository _genericRepository;

    public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository, IGenericRepository genericRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _genericRepository = genericRepository;
    }


    // GET: Products
    public async Task<IActionResult> Index(ProductVm? product, string? searchString=null)
    {
        var categories = await _categoryRepository.GetAll()!;

        if(categories is null)
        {
            categories = new List<Category>();

        }

        var categoryList = categories!.Select(c => new SelectListItem()
        {
            Text = c.Name,
            Value = c.Id.ToString()
        }).ToList();

        
        // ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name");
            
        var productList = await _productRepository.GetAll(includeProperties:"Category,Generic")!;

        if(productList is null)
        {
            productList = new List<Product>();
        }

        if(searchString is not null)
            productList = productList!.Where(p => p.Name.ToLower().Contains(searchString.ToLower()));

        if (product!.SelectedCategory is not null && product.SelectedCategory.ToLower() != "all")
            productList = productList!.Where(p => p.Category!.Id.ToString() == product.SelectedCategory);



        var productVm = new ProductVm()
        {
            CategoryList = categoryList,
            Products = productList!.ToList()
        };           
            

        return View(productVm);
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        var product = await _productRepository.GetFirstOrDefault(c => c.Id == id)!;

        if (product == null || id == null)
            return NotFound();

        return View(product);
    }

    // GET: Products/Create
    public IActionResult Create()
    {
        var categoryList = _categoryRepository.GetAll()!.Result;
        ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name");
        
        // ViewBag["CategoryId"] = new SelectList(categoryList, "Id", "Name");
        
        var genericList = _genericRepository.GetAll()!.Result;
        ViewData["GenericId"] = new SelectList(genericList, "Id", "Name");
            
        return View();
    }

    // POST: Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Strength,Generic,Details,CategoryId,GenericId")] Product product)
    {
        var categoryList = _categoryRepository.GetAll()!.Result;
        ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name");

        var genericList = _genericRepository.GetAll()!.Result;
        ViewData["GenericId"] = new SelectList(genericList, "Id", "Name");

        var existsProduct = await _productRepository.GetByName(product.Name)!;
        
        if (existsProduct is not null)
        {
            ModelState.AddModelError(string.Empty, product.Name + " already exists");
        }
            
        
        if (!ModelState.IsValid) return View(product);
        
        await _productRepository.Add(product);
        await _productRepository.Save();
                
        return RedirectToAction(nameof(Index));

    }

    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        var product = await _productRepository.GetFirstOrDefault(p => p.Id == id, includeProperties:"Category,Generic")!;

        if (product == null || id == null)
        {
            return NotFound();
        }
            
        var categoryList = await _categoryRepository.GetAll()!;
        ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name");
        
        var genericList = _genericRepository.GetAll()!.Result;
        ViewData["GenericId"] = new SelectList(genericList, "Id", "Name");

        return View(product);
    }

    // POST: Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Strength,Generic,Details,CategoryId,GenericId")] Product product)
    {
        var categoryList = _categoryRepository.GetAll()!.Result;
        ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name");
        
        if (id != product.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid) return View(product);

        _productRepository.Update(product);
        await _productRepository.Save();

        return RedirectToAction(nameof(Index));
    }

    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        var product = await _productRepository.GetFirstOrDefault(c => c.Id == id, includeProperties: "Category")!;

        if (product == null || id == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _productRepository.GetFirstOrDefault(c => c.Id == id, includeProperties: "Category")!;

        if (product != null)
        {
            _productRepository.Remove(product);
            await _categoryRepository.Save();
        }

        return RedirectToAction(nameof(Index));
    }

    


    #region API CALLS

    public async Task<IActionResult> GetAll(string? id)
    {
        var productList = await _productRepository.GetAll(includeProperties: "Category")!;

        if (id is not null && id.ToLower() != "all")
            productList = productList!.Where(p => p.CategoryId.ToString() == id);

        return Json(new { data = productList });
    }

    #endregion
    
}