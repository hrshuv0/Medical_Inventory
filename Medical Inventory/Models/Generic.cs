using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Medical_Inventory.Models
{
    public class Generic
    {
        public long Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [ValidateNever]
        public IEnumerable<Product>? Products { get; set; }

    }
}
