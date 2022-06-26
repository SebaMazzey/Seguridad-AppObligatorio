using System;

namespace DemoSeguridad.Models
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RoleViewModel Role { get; set; }
    }
}
