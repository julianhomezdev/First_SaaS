using SaaS.src.Core.Entities;

namespace SaaS.src.Application.Interfaces.TenantInterfaces
{
    public interface ITenantRepository
    {

        Task<Tenant> GetIdByAsync(int tenantId);

        Task<bool> ExistsAsync(int tenantId);

        Task<bool> ExistsByNameAsync(string name);

        Task<Tenant> CreateTenantAsync(string tenantName);


    }
}
