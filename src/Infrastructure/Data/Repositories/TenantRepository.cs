using Microsoft.EntityFrameworkCore;
using SaaS.src.Application.DTOs.Requests.Tenant;
using SaaS.src.Application.Interfaces.TenantInterfaces;
using SaaS.src.Domain.Entities;
using SaaS.src.Infrastructure.Persistence;

namespace SaaS.src.Infrastructure.Data.Repositories
{
    public class TenantRepository : ITenantRepository
    {

        private readonly AppDbContext _context;

        public TenantRepository(AppDbContext context)
        {

            _context = context;

        }


        

        public async Task<Tenant> CreateTenantAsync(CreateTenantRequest request)
        {

            var existingTenant = await ExistsByNameAsync(request.TenantName);


            if(existingTenant != null)
            {
                throw new InvalidOperationException($"There is already a tenant with the name {request.TenantName}");
            }
            

            // Create new entity
            var tenant = new Tenant(request.TenantName, request.TenantIdentifier, request.TenantDescription);


            // Save into bd
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();


            return tenant;

        }

        public async Task<bool> ExistsAsync(int tenantId)
        {

            return await _context.Tenants
                .AnyAsync(t => t.Id == tenantId);

        }

        public async Task<Tenant> ExistsByNameAsync(string name)
        {

            return await _context.Tenants
                .FirstOrDefaultAsync(t => t.TenantName.ToLower() == name.ToLower());
        }

        public async Task<Tenant> GetIdByAsync(int tenantId)
        {
            return await _context.Tenants
                .FirstOrDefaultAsync(t => t.Id == tenantId);
        }
    }
}
