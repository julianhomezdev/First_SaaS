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

        public async Task CreateSchemaPerTenant(CreateTenantRequest request)
        {

            


            var sql = $@"

                        IF NOT EXISTS (SELECT * FROM sys.Schemas WHERE name = '{request.TenantName}')
                        BEGIN
                            EXEC ('CREATE SCHEMA {request.TenantName}')
                        END
                    ";

            var schema = await _context.Database.ExecuteSqlRawAsync(sql);

            
        }

        // 

        public async Task CreateTablesInSchema(string schemaName)
        {

            schemaName = schemaName.Trim();


            var createTablesSql = $@"

                                        
                                        IF NOT EXISTS(SELECT * FROM sys.tables t
                                                        JOIN sys.schemas s ON t.schema_id = s.schema_id
                                                         WHERE s.name = '{schemaName}' AND t.name = 'Users')

                                        BEGIN 
                                               
                                            CREATE TABLE [{schemaName}].[Users]
                                            (
                                                   Id INT PRIMARY KEY(1,1),
                                                   UserName VARCHAR(100) NOT NULL,
Email NVARCHAR(100) NOT NULL,
CreatedAt DATETIME2 DEFAULT GETDATE(),
IsActive BIT DEFAULT 1

                                            )
                                                


                                    ";


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
