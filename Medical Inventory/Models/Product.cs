﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Medical_Inventory.Models;

public class Product
{
    public long Id { get; set; }
    
    [Required]
    [MaxLength(25), MinLength(1)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(10, ErrorMessage ="max length must be 10")]
    public string Strength { get; set; }

    [Display(Name = "Generic")]
    public long? GenericId { get; set; }  
    [ForeignKey("GenericId")]
    [ValidateNever]
    public Generic? Generic { get; set; }


    [Column(TypeName ="ntext")]
    public string? Details { get; set; }

 
    [Display(Name = "Category")]
    public long? CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category? Category { get; set; }
    
    [Display(Name = "Company")]
    public long? CompanyId { get; set; }
    [ForeignKey("CompanyId")]
    [ValidateNever]
    public Company? Company { get; set; }
    
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }

    /*public string? CreatedById { get; set; }
    [ForeignKey("CreatedById")]
    [ValidateNever]
    public IdentityUser? CreatedBy { get; set; }

    public string? UpdatedById { get; set; }
    [ForeignKey("UpdatedById")]
    [ValidateNever]
    public IdentityUser? UpdatedBy { get; set; }*/
    
    
    
    
}