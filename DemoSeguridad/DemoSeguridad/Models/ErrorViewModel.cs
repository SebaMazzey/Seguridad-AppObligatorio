using DemoSeguridad.Data.Entities;
using System;
using System.Collections.Generic;

namespace DemoSeguridad.Models
{
    public class HomeModel
    {
        public ICollection<Role> Roles { get; set; }
    }
}
