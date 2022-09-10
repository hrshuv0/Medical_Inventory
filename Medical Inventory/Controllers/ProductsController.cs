using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Medical_Inventory.Data;
using Medical_Inventory.Data.IRepository;
using Medical_Inventory.Models;
using Medical_Inventory.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Medical_Inventory.Exceptions;

namespace Medical_Inventory.Controllers;

[Authorize]
public class ProductsController : Controller
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IGenericRepository _genericRepository;
    private readonly ICompanyRepository _companyRepository;

    public ProductsController(ILogger<ProductsController> logger, IProductRepository productRepository, ICategoryRepository categoryRepository, IGenericRepository genericRepository, ICompanyRepository companyRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _genericRepository = genericRepository;
        _companyRepository = companyRepository;
    }


    // GET: Products
    public async Task<IActionResult> Index(ProductVm? product, string? searchString=null)
    {
        var productVm = new ProductVm();
        try
        {
            var categories = await _categoryRepository.GetAll()!;

            if (categories is null)
            {
                categories = new List<Category>();

            }

            var categoryList = categories!.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();


            // ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name");

            var productList = await _productRepository.GetAll(includeProperties: "Category,Generic,Company")!;

            if (productList is null)
            {
                productList = new List<Product>();
            }

            if (searchString is not null)
                productList = productList!.Where(p => p.Name.ToLower().Contains(searchString.ToLower()));

            if (product!.SelectedCategory is not null && product.SelectedCategory.ToLower() != "all")
                productList = productList!.Where(p => p.Category!.Id.ToString() == product.SelectedCategory);

            productVm.CategoryList = categoryList;
            productVm.Products = productList!.ToList();

        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to load product list");
        }

        return View(productVm);
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        try
        {
            var product = await _productRepository.GetFirstOrDefault(c => c.Id == id, includeProperties: "Category,Generic,Company")!;
            var createdBy = _productRepository.GetFirstOrDefaultUser(product.CreatedById!);
            var updatedBy = _productRepository.GetFirstOrDefaultUser(product.UpdatedById!);

            product.CreatedBy = createdBy;
            product.UpdatedBy = updatedBy;

            return View(product);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning($"product not found with id: {id}");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }

        return RedirectToAction("PageNotFound", "Home");
    }

    // GET: Products/Create
    [Authorize(Roles = StaticData.RoleAdmin)]
    public IActionResult Create()
    {
        try
        {
            var categoryList = _categoryRepository.GetAll()!.Result;
            ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name");

            // ViewBag["CategoryId"] = new SelectList(categoryList, "Id", "Name");

            var genericList = _genericRepository.GetAll()!.Result;
            ViewData["GenericId"] = new SelectList(genericList, "Id", "Name");

            var companyList = _companyRepository.GetAll()!.Result;
            ViewData["CompanyId"] = new SelectList(companyList, "Id", "Name");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);            
        }
            
        return View();
    }

    // POST: Products/Create
    [Authorize(Roles = StaticData.RoleAdmin)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Strength,Generic,Details,CategoryId,GenericId, CompanyId")] Product product)
    {
        try
        {
            var categoryList = _categoryRepository.GetAll()!.Result;
            ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name");

            var genericList = _genericRepository.GetAll()!.Result;
            ViewData["GenericId"] = new SelectList(genericList, "Id", "Name");

            var companyList = _companyRepository.GetAll()!.Result;
            ViewData["CompanyId"] = new SelectList(companyList, "Id", "Name");

            var existsProduct = await _productRepository.GetByName(product.Name)!;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            product.CreatedTime = DateTime.Now;
            product.UpdatedTime = DateTime.Now;
            product.CreatedById = userId;
            product.UpdatedById = userId;

            await _productRepository.Add(product);
            await _productRepository.Save();

            _logger.LogInformation(message: $"new category added with name of {product.Name}");
        }
        catch (DuplicationException ex)
        {
            ModelState.AddModelError(string.Empty, product.Name + " already exists");
            _logger.LogWarning(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }

        if (!ModelState.IsValid) return View(product);
                
        return RedirectToAction(nameof(Index));

    }

    // GET: Products/Edit/5
    [Authorize(Roles = StaticData.RoleAdmin)]
    public async Task<IActionResult> Edit(long? id)
    {
        try
        {
            var product = await _productRepository.GetFirstOrDefault(p => p.Id == id, includeProperties: "Category,Generic,Company")!;

            var categoryList = await _categoryRepository.GetAll()!;
            ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name");

            var genericList = _genericRepository.GetAll()!.Result;
            ViewData["GenericId"] = new SelectList(genericList, "Id", "Name");

            var companyList = _companyRepository.GetAll()!.Result;
            ViewData["CompanyId"] = new SelectList(companyList, "Id", "Name");

            return View(product);
        }
        catch (NotFoundException )
        {
            _logger.LogWarning($"category not found of id: {id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        return RedirectToAction("PageNotFound", "Home");        
    }

    // POST: Products/Edit/5
    [Authorize(Roles = StaticData.RoleAdmin)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Strength,Generic,Details,CategoryId,GenericId, CompanyId")] Product product)
    {
        // var categoryList = _categoryRepository.GetAll()!.Result;
        // ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name");
        try
        {
            //var existsCategory = await _productRepository.GetByName(product.Name)!;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            product.UpdatedById = userId;

            _productRepository.Update(product);
            await _productRepository.Save();

            _logger.LogWarning($"product updated of id: {id}");
        }
        catch (NotFoundException)
        {
            _logger.LogWarning($"product not found of id: {id}");
            return NotFound();
        }
        catch (DuplicationException ex)
        {
            _logger.LogWarning($"category alaready exists of name: {product.Name}");
            ModelState.AddModelError(string.Empty, product.Name + " already exists");
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Failed to update data of id: {id}");
            _logger.LogWarning(ex.Message);
        }

        if (!ModelState.IsValid) return View(product);

        return RedirectToAction(nameof(Index));
    }

    // GET: Products/Delete/5
    [Authorize(Roles = StaticData.RoleAdmin)]
    public async Task<IActionResult> Delete(long? id)
    {
        try
        {
            var product = await _productRepository.GetFirstOrDefault(c => c.Id == id, includeProperties: "Category,Generic,Company")!;
            
            return View(product);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning($"product not found with id: {id}");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }

        return RedirectToAction("PageNotFound", "Home");

    }

    // POST: Products/Delete/5
    [Authorize(Roles = StaticData.RoleAdmin)]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        try
        {
            var product = await _productRepository.GetFirstOrDefault(c => c.Id == id, includeProperties: "Category,Generic,Company")!;

            _productRepository.Remove(product);
            await _productRepository.Save();

            return RedirectToAction(nameof(Index));
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning($"product not found with id: {id}");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }

        return RedirectToAction("PageNotFound", "Home");
    }

    


    #region API CALLS

    [Authorize]
    [Route("api/[controller]")]
    public async Task<IActionResult> GetAll(string? id)
    {
        try
        {
            var productList = await _productRepository.GetAll(includeProperties: "Category,Generic,Company")!;
            productList = productList!.OrderByDescending(p => p.UpdatedTime);

            if (id is not null && id.ToLower() != "all")
                productList = productList!.Where(p => p.CategoryId.ToString() == id);

            return Json(new { data = productList });
        }
        catch (Exception)
        {
            _logger.LogError("failed to load data");
        }

        return Json(new { data = "" });
    }

    #endregion
    
}