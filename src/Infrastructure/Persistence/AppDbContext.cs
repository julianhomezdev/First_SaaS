using Microsoft.EntityFrameworkCore;
using SaaS.src.Domain.Entities; 
namespace SaaS.src.Infrastructure.Persistence 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.TenantIdentifier)
                    .IsUnique();

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                entity.Property(e => e.TenantDateCreated)
                    .HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}