using System.ComponentModel.DataAnnotations;

namespace DemoSeguridad.Data.Entities
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public string HashSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public Role Role { get; set; }
    }
}