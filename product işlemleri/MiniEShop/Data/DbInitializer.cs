using System;
using Microsoft.AspNetCore.Identity;
using MiniEShop.Models;

namespace MiniEShop.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = ["Admin","Customer"];
        foreach(var role in roles)
        {
            if(!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // User bilgilerini seed edeceğiz.

        await SeedUserAsync(userManager, "admin@eshop.local","Qwe123.,","Kemal Kükrer","Admin");
        await SeedUserAsync(userManager, "customer1@eshop.local", "Qwe123.,","Ali Candan", "Customer");
        await SeedUserAsync(userManager, "customer2@eshop.local", "Qwe123.,", "Ceren Küçük", "Customer");
    }

    private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string email, string password, string fullName, string role)
    {
        var user = await userManager.FindByEmailAsync(email);
        if(user is not null) return;
        user = new ApplicationUser
        {
            UserName=email,
            Email=email,
            FullName=fullName,
            EmailConfirmed=true  
        };

        var result = await userManager.CreateAsync(user, password);
        if(result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, role);
        }

    }
}
