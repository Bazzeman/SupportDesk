using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SupportDesk.Data;

namespace SupportDesk.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task MigrateDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.MigrateAsync();
        }

        public static async Task SeedAuthorizationRolesAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(ApplicationUserRoles.Client))
            {
                await roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.Client));
            }
            if (!await roleManager.RoleExistsAsync(ApplicationUserRoles.Staff))
            {
                await roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.Staff));
            }
            if (!await roleManager.RoleExistsAsync(ApplicationUserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.Admin));
            }
        }
    }
}
