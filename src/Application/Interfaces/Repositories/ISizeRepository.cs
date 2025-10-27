using SaaS.src.Domain.Entities;

namespace SaaS.src.Application.Interfaces.Repositories
{
    public interface ISizeRepository
    {

        Task<List<Size>> GetAllSizesAsync();
        Task<Size?> GetSizeByIdAsync(int id);
    }
}
