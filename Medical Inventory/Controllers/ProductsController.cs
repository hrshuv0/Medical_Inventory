using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Medical_Inventory.Data;
using Medical_Inventory.Data.IRepository;
using Medical_Inventory.Models;

namespace Medical_Inventory.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }


        // GET: Products
        public async Task<IActionResult> Index(string? searchString=null)
        {
            var productList = await _productRepository.GetAll(includeProperties:"Category");

            if(searchString is not null)
                productList = productList.Where(p => p.Name.ToLower().Contains(searchString.ToLower()));

            return View(productList);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var product = await _productRepository.GetFirstOrDefault(c => c.Id == id);

            if (product == null || id == null)
                return NotFound();

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var categoryList = _categoryRepository.GetAll().Result;
            ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name");
            
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Strength,Generic,Details,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.Add(product);
                await _productRepository.Save();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var product = await _productRepository.GetFirstOrDefault(p => p.Id == id);
            if (product == null || id == null)
            {
                return NotFound();
            }
            
            var categoryList = _categoryRepository.GetAll().Result;
            ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name");
            
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Strength,Generic,Details,CategoryId")] Product product)
        {
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
            var product = await _productRepository.GetFirstOrDefault(c => c.Id == id);

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
            var product = await _productRepository.GetFirstOrDefault(c => c.Id == id);

            if (product != null)
            {
                _productRepository.Remove(product);
                await _categoryRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
