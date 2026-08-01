using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupportDesk.Data.Entities;

namespace SupportDesk.Data
{
    public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(e =>
            {
                e.Property(u => u.FullName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            builder.Entity<Ticket>(e =>
            {
                e.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                e.Property(t => t.Description)
                    .IsRequired()
                    .HasMaxLength(2000);
            });
        }
    }
}
