using Microsoft.EntityFrameworkCore;
using SaaS.src.Application.Interfaces.Repositories;
using SaaS.src.Infrastructure.Persistence;

namespace SaaS.src.Infrastructure.Data.Repositories.Size
{
    public class SizeRepository : ISizeRepository
    {
        private readonly AppDbContext _context;


        public SizeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SaaS.src.Domain.Entities.Size>> GetAllSizesAsync()
        {
            return await _context.Sizes.ToListAsync();
        }

        public async Task<SaaS.src.Domain.Entities.Size?> GetSizeByIdAsync(int id)
        {
            return await _context.Sizes.FindAsync(id);
        }

        public async Task<SaaS.src.Domain.Entities.Size> CreateSizeAsync(SaaS.src.Domain.Entities.Size size)
        {

            await _context.Sizes.AddAsync(size);
            await _context.SaveChangesAsync();
            return size;


        }
    }
}
