using Microsoft.AspNetCore.Identity;

namespace Medical_Inventory.Models;


public class ApplicationUser : IdentityUser<long>
{
    public string? FullName { get; set; }
}

public class ApplicationRole : IdentityRole<long>
{
    public ApplicationRole() : base()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}