using Microsoft.AspNetCore.Identity;
using SupportDesk.Data;
using SupportDesk.Data.Dtos;
using SupportDesk.Data.Entities;

namespace SupportDesk.Services
{
    public sealed class AccountService(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager, 
        ApplicationDbContext dbContext)
    {
        public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe, bool lockoutOnFailure = false) =>
            await signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure);

        public async Task LogoutAsync() =>
            await signInManager.SignOutAsync();

        public async Task<IdentityResult> RegisterAsync(string email, string fullName, string password)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                CreationDate = DateOnly.FromDateTime(DateTime.UtcNow)
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

        public async Task<AccountOverviewDto?> GetAccountOverview()
        {
            ApplicationUser? account = await userManager.GetUserAsync(signInManager.Context.User);

            if (account is null)
            {
                return null;
            }

            var role = (await userManager.GetRolesAsync(account)).FirstOrDefault() ?? string.Empty;
            var email = account.Email ?? string.Empty;

            return new AccountOverviewDto(account.FullName, email, role, account.CreationDate);
        }
    }
}
