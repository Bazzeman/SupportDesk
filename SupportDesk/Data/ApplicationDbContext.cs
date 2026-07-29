using Microsoft.EntityFrameworkCore;
using SupportDesk.Models.Entities;

namespace SupportDesk.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
