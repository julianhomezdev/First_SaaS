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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
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
            var query = _context.Products.Where(p => p.ProductReference == reference);

            if (excludeId.HasValue)
            {
                query = query.Where(p => p.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync(List<int>? sizeIds = null)
        {
            var query = _context.Products
                .Include(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
                .AsNoTracking();

            
            if (sizeIds != null && sizeIds.Any())
            {
                query = query.Where(p => p.ProductSizes.Any(ps => sizeIds.Contains(ps.SizeId)));
            }

            return await query.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {

            return await _context.Products
                .Include(p => p.ProductSizes) 
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task DeleteAsync(int productId)
        {

            var product = await _context.Products
                .Include(p => p.ProductSizes)
                .FirstOrDefaultAsync(p => p.Id == productId);


            if(product != null)
            {


                _context.ProductsSizes.RemoveRange(product.ProductSizes);

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

            }

        }
    }
}
