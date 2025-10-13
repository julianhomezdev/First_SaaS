using SaaS.src.Application.Interfaces.SchemaInterfaces;
using SaaS.src.Infrastructure.Data.Repositories;
using SaaS.src.Infrastructure.Persistence;

namespace SaaS.src.Infrastructure.Services.Schema
{
    public class SchemaService : ISchemaService
 

    {
        // AppDbContext is the one who knows where to look
        private readonly AppDbContext _context;
        private readonly ILogger<SchemaService> _logger;
        private readonly TenantRepository _tenantRepository;

        public SchemaService(AppDbContext context, ILogger<SchemaService> logger, TenantRepository tenantRepository)
        {
            _context = context;
            _logger = logger;
            _tenantRepository = tenantRepository;

        }


        public async Task CreateTenantSchema(string tenantName)
        {


            if (string.IsNullOrEmpty(tenantName))
                throw new ArgumentException("The tenant name cannot be empty");

            if (tenantName.Length < 3)
                throw new ArgumentException("The tenant name must have at least 3 characters");



            //var tenant = await _tenantRepository.CreateTenantAsync(tenantName);


            
           
        }

        public Task<string> GetTenantSchema(int tenantId)
        {
            throw new NotImplementedException();
        }
    }
}
