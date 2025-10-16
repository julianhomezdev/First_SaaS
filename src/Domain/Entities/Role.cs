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
        [MaxLength(100)]
        public string RoleName { get; set; }

        // Role description
        public string RoleDescription { get; set; }


        // All users that has a specific role
        // It means that a role can have multiple users
        public virtual ICollection<User> Users { get; set; }


    }
}
