using System.ComponentModel.DataAnnotations;

namespace SaaS.src.Domain.Entities
{
    public class User
    {

        [Key]
        // Pk
        public int Id { get; set; }

        // User name
        [Required]
        public string UserName  { get; set; }

        // User LastName
        [Required]
        public string LastName { get; set; }


        // User dni
        [Required]
        [MaxLength(10)]
        public string UserDni { get; set; }


        // Fk role
        public int UserRoleId { get; set; }



        public Role UserRole { get; set; }


        // Datetime created
        public DateTime? UserCreateDate { get; set; }










    }
}
