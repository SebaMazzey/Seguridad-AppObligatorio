using System;
using System.Collections.Generic;

namespace DemoSeguridad.Models
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            Permissions = new HashSet<PermissionViewModel>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PermissionViewModel> Permissions { get; set; }
    }
}
