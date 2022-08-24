using Microsoft.AspNetCore.Mvc.Rendering;

namespace Medical_Inventory.Models.ViewModel;

public class ProductVm
{
    public ProductVm()
    {
        Products = new List<Product>();
    }
    public string? SelectedCategory { get; set; }

    public IEnumerable<SelectListItem> CategoryList { get; set; }
    
    public IEnumerable<Product> Products { get; set; }

   
}