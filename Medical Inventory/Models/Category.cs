using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medical_Inventory.Models;

public class Category
{
    public long Id { get; set; }
    
    [Required]
    [StringLength(20)]
    public string Name { get; set; }

    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }

    public long? CreatedById { get; set; }
    [ForeignKey("CreatedById")]
    [ValidateNever]
    public ApplicationUser? CreatedBy { get; set; }

    public long? UpdatedById { get; set; }
    [ForeignKey("UpdatedById")]
    [ValidateNever]
    public ApplicationUser? UpdatedBy { get; set; }

    [ValidateNever]
    public IEnumerable<Product>? Products { get; set; }
}