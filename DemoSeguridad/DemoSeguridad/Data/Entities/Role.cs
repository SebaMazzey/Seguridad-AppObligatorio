using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoSeguridad.Data.Entities
{
    public class Role
    {
        public Role()
        {
            RolePermissions = new HashSet<RolePermission>();
            Users = new HashSet<User>();
        }

        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
        public ICollection<User> Users { get; set; }
    }
}