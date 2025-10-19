using Microsoft.EntityFrameworkCore;
using SaaS.src.Domain.Entities; 
namespace SaaS.src.Infrastructure.Persistence 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


   


            // Roles table config
            modelBuilder.Entity<Role>(entity =>
            {


                entity.HasKey(r => r.Id);
                    
                


            });


            // Users table config
            modelBuilder.Entity<User>(entity =>


                {
                    entity.HasKey(e => e.Id);

                    // The dni is unique
                    entity.HasIndex(e => e.UserDni)
                        .IsUnique();


                    entity.Property(e => e.UserCreateDate)
                        .HasDefaultValueSql("GETUTCDATE()");
                       


                    // Config fk
                    entity.HasOne(u => u.UserRole)
                        .WithMany(r => r.Users)
                        .HasForeignKey(u => u.UserRoleId)
                        .OnDelete(DeleteBehavior.Restrict);


                });



        }
    }
}