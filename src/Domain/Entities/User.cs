using System.ComponentModel.DataAnnotations;

namespace SaaS.src.Domain.Entities
{
    public class User
    {

        public int Id { get; set; }
        
        public string Email { get; set; }

        public string IdentificationNumber { get; set; }

        public string PasswordHash { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    }
}
