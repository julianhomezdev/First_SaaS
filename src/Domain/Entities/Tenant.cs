using System.ComponentModel.DataAnnotations;

namespace SaaS.src.Domain.Entities
{
    // Tenant client
    public class Tenant 
    {

        [Key]
        // Pk
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        // Unique identifier for invoices like (MW-001)
        public string TenantIdentifier { get; set; }

        // Tenant name
        [Required]
        public string TenantName { get; set; }

        // Short client description
        public string TenantDescription { get; set; }

        // Status tenant
        public bool IsActive { get; set; } = true;

        // Tenant created date
        public DateTime? TenantDateCreated { get; set; }

        //public string SchemaName { get; set; }


        public Tenant()
        {

        }


        public Tenant(string name, string tenantIdentifier, string tenantDescription)
        {

            TenantName = name;
            TenantIdentifier = tenantIdentifier;
            TenantDescription = tenantDescription;


        }

  







    }
}
