using SaaS.src.Application.Interfaces.TenantInterfaces;
using SaaS.src.Core.Entities;
using SaaS.src.Infrastructure.Persistance;

namespace SaaS.src.Infrastructure.Data.Repositories
{
    public class TenantRepository : ITenantRepository
    {

        private readonly AppDbContext _context;

        public TenantRepository(AppDbContext context)
        {

            _context = context;

        }


        

        public async Task<Tenant> CreateTenantAsync(string tenantName)
        {


            if(await ExistsByNameAsync(tenantName))
            {

                throw new InvalidOperationException($"There is already a tenant with the name {tenantName}");

            }


            // Create new entity
            var tenant = new Tenant(tenantName);


            // Save into bd
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();


            return tenant;

        }

        public Task<bool> ExistsAsync(int tenantId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Tenant> GetIdByAsync(int tenantId)
        {
            throw new NotImplementedException();
        }
    }
}
