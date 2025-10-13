namespace SaaS.src.Application.Interfaces.SchemaInterfaces
{
    public interface ISchemaService
    {



        // Create a schema for a new tenant
        Task CreateTenantSchema(string tenantName);

        // Receives the tenant id and returns the schema name of that tenant
        Task <string> GetTenantSchema(int tenantId);


    }
}
