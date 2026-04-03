using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Infrastruture.Data.Seeders
{
    public class IdentitySeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("Teacher"))
            {
                await roleManager.CreateAsync(new IdentityRole("Teacher"));
            }
            if (!await roleManager.RoleExistsAsync("Student"))
            {
                await roleManager.CreateAsync(new IdentityRole("Student"));
            }
        }
    }
}
