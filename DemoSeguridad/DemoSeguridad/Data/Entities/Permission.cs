using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoSeguridad.Data.Entities
{
    public class Permission
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}