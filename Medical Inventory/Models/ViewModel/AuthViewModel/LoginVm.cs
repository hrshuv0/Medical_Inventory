using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Medical_Inventory.Models.ViewModel.AuthViewModel
{
    public class LoginVm
    {
        [Required(ErrorMessage = "user name is required")]
        [Display(Name = "User Name")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
