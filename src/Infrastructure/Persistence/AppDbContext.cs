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

        public DbSet<Product> Products { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<ProductsSizes> ProductsSizes { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; } 

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceItem> InvoiceItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // ProductTypes config
            modelBuilder.Entity<ProductType>(entity =>
            {

                entity.HasKey(pt => pt.Id);

                entity.Property(pt => pt.ProductTypeName)
                    .IsRequired()
                    .HasMaxLength(256);



            });


            // ProductsSizes config
            modelBuilder.Entity<ProductsSizes>(entity =>
            {

                entity.HasKey(ps => ps.Id);


                // Unique index 
                entity.HasIndex(ps => new { ps.ProductId, ps.SizeId })
                    .IsUnique();

                // Product relation
                entity.HasOne(ps => ps.Product)
                     .WithMany(p => p.ProductSizes)
                     .HasForeignKey(ps => ps.ProductId)
                     .OnDelete(DeleteBehavior.Cascade);

                // Size relation
                entity.HasOne(ps => ps.Size)
                    .WithMany(s => s.ProductSizes)
                    .HasForeignKey(ps => ps.SizeId)
                    .OnDelete(DeleteBehavior.Cascade);


                // Stock must be positive
                entity.Property(ps => ps.SizeStock)
                    .IsRequired();


            });
            



            // Sizes table config
            modelBuilder.Entity<Size>(entity =>
            {


                entity.HasKey(s => s.Id);

                entity.Property(s => s.SizeName)
                    .IsRequired()
                    .HasMaxLength(50);


            });



            // Roles table config
            modelBuilder.Entity<Role>(entity =>
            {


                entity.HasKey(r => r.Id);
                    
                


            });


            // Users table config
            modelBuilder.Entity<User>(entity =>


                {
                    entity.HasKey(u => u.Id);


                    // User name config
                    entity.Property(u => u.UserName)
                        .IsRequired();

                    // User lastname config
                    entity.Property(u => u.UserLastName)
                        .IsRequired();


                    // DNI CONFIG
                    // The dni is unique
                    entity.HasIndex(u => u.UserDni)
                        .IsUnique();

                    entity.Property(u => u.UserDni)
                        .HasMaxLength(10);


                    entity.Property(u => u.UserCreateDate)
                        .HasDefaultValueSql("GETUTCDATE()");
                       

                    // Config fk
                    entity.HasOne(u => u.UserRole)
                        .WithMany(r => r.Users)
                        .HasForeignKey(u => u.UserRoleId)
                        .OnDelete(DeleteBehavior.Restrict);


                });


            // Product entity config
            modelBuilder.Entity<Product>(entity =>
            {

                entity.HasKey(p => p.Id);

                // Product name config

                entity.HasIndex(p => p.ProductName);

                entity.Property(p => p.ProductName)
                    .IsRequired()
                    .HasMaxLength(250);

                // Product price config
                entity.Property(p => p.ProductPrice)
                    .IsRequired()
                    .HasColumnType("decimal(18,3)");

                // Product quantity config
                entity.Ignore(p => p.ProductTotalQuantity);

                // Relation with ProductType(1:N)
                entity.HasOne(p => p.ProductType)
                    .WithMany(pt => pt.Products)
                    .HasForeignKey(p => p.ProductTypeId)
                    .OnDelete(DeleteBehavior.Restrict);


            });

            modelBuilder.Entity<Invoice>(entity =>
            {

                entity.HasKey(e => e.Id);
                entity.Property(e => e.InvoiceNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.CustomerDocument).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Subtotal).HasColumnType("decimal(18,3)");
                entity.Property(e => e.Tax).HasColumnType("decimal(18,3)");
                entity.Property(e => e.Total).HasColumnType("decimal(18,3)");


            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,3)");
                entity.Property(e => e.Total).HasColumnType("decimal(18,3)");

                entity.HasOne(e => e.Invoice)
                    .WithMany(i => i.Items)
                    .HasForeignKey(e => e.InvoiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


        }
    }
}