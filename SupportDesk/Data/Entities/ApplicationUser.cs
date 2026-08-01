using Microsoft.AspNetCore.Identity;

namespace SupportDesk.Data.Entities
{
    public sealed class ApplicationUser : IdentityUser
    {
        public required string FullName { get; set; }
    }
}
