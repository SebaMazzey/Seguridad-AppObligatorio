using DemoSeguridad.Data;
using Microsoft.AspNetCore.Mvc;

namespace DemoSeguridad.Authorization
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(params string[] permissions) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] {new AuthorizeFilter(permissions) };
        }
    }
}