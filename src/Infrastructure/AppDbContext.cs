using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Identity.Client;
using SaaS.src.Core.Entities;

namespace SaaS.src.Infrastructure
{
    public class AppDbContext : DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)

        { }



            public DbSet<Tenant> Tenants { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // Tenant config
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

            // Tenant user
            modelBuilder.Entity<User>(entity =>
            {

                entity.HasKey(u => u.Id);

                entity.HasIndex(u => u.UserIdentification)
                    .IsUnique();

                entity.HasOne(u => u.Tenant)
                    .WithMany(t => t.Users)
                    .HasForeignKey(u => u.TenantId)
                    // If one tenant is deleted her users too
                    .OnDelete(DeleteBehavior.Cascade);





            });
        }

    }
        
        
}

   

