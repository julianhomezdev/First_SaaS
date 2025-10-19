using System.ComponentModel.DataAnnotations;

namespace SaaS.src.Domain.Entities
{
    public class User
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string UserDni { get; set; }
        public int UserRoleId { get; set; }
        public Role UserRole { get; set; }
        public DateTime? UserCreateDate { get; set; }



    }
}
