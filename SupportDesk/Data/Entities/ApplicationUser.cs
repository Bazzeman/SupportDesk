using Microsoft.AspNetCore.Identity;

namespace SupportDesk.Data.Entities
{
    public sealed class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string Infix { get; set; }
        public required string LastName { get; set; }
    }
}
