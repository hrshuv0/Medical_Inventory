using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Medical_Inventory.Models.ViewModel.AuthViewModel
{
    public class RegisterVm
    {
        [Required(ErrorMessage = "user name is required")]
        [Display(Name = "User Name")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "minimum length 6 required")]
        public string? Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "password didn't matched")]
        public string? ConfirmPassword { get; set; }
    }
}
