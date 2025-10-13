using SaaS.src.Application.DTOs.Requests.Tenant;
using SaaS.src.Domain.Entities;

namespace SaaS.src.Application.Interfaces.TenantUseCases
{
    public interface ICreateTenantUseCase
    {


        // Defines what to do but not how

        Task<Tenant> CreateAsync(CreateTenantRequest request);

    }
}
