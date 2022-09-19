using Entities;
using Inventory.DAL.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.DAL.Seed;

public static class DbInitializer
{
    public static async Task Initialize(IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices.CreateScope();

        var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }

        }
        catch (Exception )
        {
            // Console.WriteLine(e);
            // throw;
        }

        try
        {
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            if (!await roleManager.RoleExistsAsync(StaticData.RoleAdmin))
            {
                await roleManager.CreateAsync(new ApplicationRole(StaticData.RoleAdmin));
                await roleManager.CreateAsync(new ApplicationRole(StaticData.RoleUser));


                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var admin = await userManager.FindByNameAsync("admin");
                if (admin is null)
                {
                    var adminUser = new ApplicationUser()
                    {
                        UserName = "admin"
                    };
                    await userManager.CreateAsync(adminUser, "123456");
                    await userManager.AddToRoleAsync(adminUser, StaticData.RoleAdmin);
                }
                var user = await userManager.FindByNameAsync("user");
                if (user is null)
                {
                    var memberUser = new ApplicationUser()
                    {
                        UserName = "user"
                    };
                    await userManager.CreateAsync(memberUser, "123456");
                    await userManager.AddToRoleAsync(memberUser, StaticData.RoleUser);
                }


            }

        }
        catch (Exception)
        {

            Console.WriteLine("faild to seed users");
        }

        

        
    }
}