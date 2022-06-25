using DemoSeguridad.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DemoSeguridad.Authorization
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(params string[] permissions) : base(typeof(AuthorizeFilter))
        {
            if (permissions.Length != 0)
                this.Arguments = new object[] { permissions };
        }
    }
}