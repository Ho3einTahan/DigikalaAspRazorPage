using Microsoft.AspNetCore.Identity;

namespace digikala_netCore.Seed
{
    public class Seed
    {

        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // IF Roll IS NOT Exist
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Check Default Admin Is Exist
            var adminUser = await userManager.FindByNameAsync("admin");

            if (adminUser == null)
            {
                // Create New Admin
                var defaultAdmin = new IdentityUser
                {
                    UserName = "admin",
                };

                var result = await userManager.CreateAsync(defaultAdmin, "admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultAdmin, "Admin");
                }

            }
        }

    }
}
