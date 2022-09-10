using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Medical_Inventory.Data.Seed;

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
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!await roleManager.RoleExistsAsync(StaticData.RoleAdmin))
            {
                await roleManager.CreateAsync(new IdentityRole(StaticData.RoleAdmin));
                await roleManager.CreateAsync(new IdentityRole(StaticData.RoleUser));


                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var admin = await userManager.FindByIdAsync("admin");
                if (admin is null)
                {
                    var adminUser = new IdentityUser()
                    {
                        UserName = "admin"
                    };
                    await userManager.CreateAsync(adminUser, "1234");
                    await userManager.AddToRoleAsync(adminUser, StaticData.RoleAdmin);
                }
                var user = await userManager.FindByIdAsync("user");
                if (user is null)
                {
                    var memberUser = new IdentityUser()
                    {
                        UserName = "user"
                    };
                    await userManager.CreateAsync(memberUser, "1234");
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