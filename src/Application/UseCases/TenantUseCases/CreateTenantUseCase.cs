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

                // Validations

                if (string.IsNullOrWhiteSpace(request.TenantName))
                    throw new ArgumentException("The tenant name is required");


                if (request.TenantName.Length > 50)
                    throw new ArgumentException("The tenant name cannot exceed 50 characters");

                



                // Delegate creation to the repository
                var tenant = await _tenantRepository.CreateTenantAsync(request);

                _logger.LogInformation("Tenant created succesfully}");


                await _tenantRepository.CreateSchemaPerTenant(request);

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
