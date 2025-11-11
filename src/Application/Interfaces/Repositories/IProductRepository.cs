using SaaS.src.Domain.Entities;

namespace SaaS.src.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {




        // Create a new product
        Task<Product> CreateProductAsync(Product product);

        Task<bool> ExistsByReferenceAsync(string reference, int? excludeId = null);

        Task<IEnumerable<Product>> GetAllAsync(List<int>? sizeIds= null);

        Task <Product> GetByIdAsync(int productId);

        Task DeleteAsync(int productId);
        Task SaveChangesAsync();





    }
}
