using CinemaManager.Models;
using Microsoft.AspNetCore.Identity;

namespace CinemaManager.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
           
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "Atendente", "Cliente" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Criar Usuário Admin 
            var adminUser = await userManager.FindByEmailAsync("admin@cinema.com");

            if (adminUser == null)
            {
                var newAdminUser = new ApplicationUser
                {
                    UserName = "admin@cinema.com",
                    Email = "admin@cinema.com",
                    FullName = "Administrador", 
                    EmailConfirmed = true 
                };

                var createPowerUser = await userManager.CreateAsync(newAdminUser, "Admin123!");
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdminUser, "Admin");
                }
            }
        }
    }
}