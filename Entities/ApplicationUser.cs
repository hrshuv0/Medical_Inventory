using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace Entities;


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