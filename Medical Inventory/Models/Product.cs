using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Medical_Inventory.Models;

public class Product
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(25), MinLength(1)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(10, ErrorMessage ="max length must be 10")]
    public string Strength { get; set; }

    [Display(Name = "Generic")]
    public int? GenericId { get; set; }
    [ForeignKey("GenericId")]
    [ValidateNever]
    public Generic? Generic { get; set; }


    [Column(TypeName ="ntext")]
    public string? Details { get; set; }

 
    [Display(Name = "Category")]
    public int? CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category? Category { get; set; }
}