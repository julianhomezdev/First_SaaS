using SaaS.src.Application.DTOs.Requests.Tenant;
using SaaS.src.Domain.Entities;

namespace SaaS.src.Application.Interfaces.TenantInterfaces
{
    public interface ITenantRepository
    {

        Task<Tenant> GetIdByAsync(int tenantId);

        Task<bool> ExistsAsync(int tenantId);

        Task<Tenant> ExistsByNameAsync(string name);

        Task<Tenant> CreateTenantAsync(CreateTenantRequest request);

        Task CreateSchemaPerTenant(CreateTenantRequest request);

        Task CreateTablesInSchema(string schemaName);


    }
}
