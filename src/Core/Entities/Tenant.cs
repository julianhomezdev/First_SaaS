using System.ComponentModel.DataAnnotations;

namespace SaaS.src.Core.Entities
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

        public ICollection<User> Users { get; set; } = new List<User>();








    }
}
