using System.ComponentModel.DataAnnotations;

namespace SaaS.src.Domain.Entities
{


    // Table in general to map the user roles
    public class Role
    {

        [Key]
        // Pk
        public int Id { get; set; }


        // Role name
        [Required]
        public string RoleName { get; set; }

        // Role description
        public string RoleDescription { get; set; }

    }
}
