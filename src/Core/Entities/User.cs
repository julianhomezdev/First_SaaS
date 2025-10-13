using System.ComponentModel.DataAnnotations;

namespace SaaS.src.Core.Entities
{
    public class User
    {


        [Key]
        // Pk
        public int Id { get; set; }

        // User name
        public string Name { get; set; }

        // User identification
        [Required]
        public string UserIdentification { get; set; }

        // User role
        public string UserRole { get; set; }

        // Created date
        public DateTime? UserCreatedDate { get; set; }


        public int TenantId { get; set; }

        // Relations
        public Tenant Tenant { get; set; }
       





    }
}
