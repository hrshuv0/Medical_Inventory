using System.ComponentModel.DataAnnotations;

namespace Medical_Inventory.Models;

public class Category
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
}