using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Entities;

public class Company
{
    public long Id { get; set; }

    [Required]
    [StringLength(20)]
    public string Name { get; set; }

    [Required]
    [StringLength(15)]
    public string Phone { get; set; }
    
    [StringLength(50)]
    public string? Address { get; set; }

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

    public IEnumerable<Product>? Products { get; set; }
    
}