using Microsoft.AspNetCore.Identity;
using SupportDesk.Data;
using SupportDesk.Data.Entities;

namespace SupportDesk.Services
{
    public sealed class ApplicationUserService(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager, 
        ApplicationDbContext dbContext)
    {
        public async Task<SignInResult> Login(string email, string password, bool rememberMe, bool lockoutOnFailure = false) =>
            await signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure);

        public async Task Logout() =>
            await signInManager.SignOutAsync();

        public async Task<IdentityResult> Register(string email, string fullName, string password)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName
            };

            IdentityResult result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return result;
            }

            result = await userManager.AddToRoleAsync(user, ApplicationUserRoles.Client);

            if (result.Succeeded)
            {
                await transaction.CommitAsync();
            }

            return result;
        }

    }
}
