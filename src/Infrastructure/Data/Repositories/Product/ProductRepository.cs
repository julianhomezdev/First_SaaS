using Microsoft.EntityFrameworkCore;
using SaaS.src.Application.Interfaces.Repositories;
using SaaS.src.Domain.Entities;
using SaaS.src.Infrastructure.Persistence;

namespace SaaS.src.Infrastructure.Data.Repositories
{ 
    public class ProductRepository : IProductRepository
    {

        private readonly AppDbContext _context;

        
        public ProductRepository(AppDbContext context)
        {

            _context = context;

        }


        // Create a product
        public async Task<Product> CreateProductAsync(Domain.Entities.Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task<bool> ExistsByReferenceAsync(string reference, int? excludeId = null)
        {

            return await _context.Products
                .AnyAsync(p => p.ProductReference.ToLower() == reference.ToLower());


        }
    }
}
