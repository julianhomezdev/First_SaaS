using SaaS.src.Application.DTOs.Requests.Tenant;
using SaaS.src.Application.Interfaces.TenantInterfaces;
using SaaS.src.Application.Interfaces.TenantUseCases;
using SaaS.src.Domain.Entities;

namespace SaaS.src.Application.UseCases.TenantUseCases
{
    public class CreateTenantUseCase : ICreateTenantUseCase
    {


        private readonly ILogger<CreateTenantUseCase> _logger;
        private readonly ITenantRepository _tenantRepository;



        public CreateTenantUseCase(ILogger<CreateTenantUseCase> logger, ITenantRepository tenantRepository)
        {

            _logger = logger;
            _tenantRepository = tenantRepository;




        }

        public async Task<Tenant> CreateAsync(CreateTenantRequest request)
        {
            
            try
            {


                _logger.LogInformation("Creating new tenant");



                // Delegate creation to the repository
                var tenant = await _tenantRepository.CreateTenantAsync(request);

                _logger.LogInformation("Tenant created succesfully}");

                // In the future, request email to send a welcome message to the SaaS

                return tenant;


            }

            catch (Exception ex) {

                _logger.LogError(ex, "Error creating tenant");
                throw;
                

            }


        }
    }
}
