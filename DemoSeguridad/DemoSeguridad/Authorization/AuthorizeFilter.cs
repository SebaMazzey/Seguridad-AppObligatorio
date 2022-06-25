using DemoSeguridad.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoSeguridad.Authorization
{
    public class AuthorizeFilter : IAuthorizationFilter
    {
        private readonly ICollection<string> permissions;

        public AuthorizeFilter(ICollection<string> permissions)
        {
            this.permissions = permissions;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Get DB context
            var dbContext = context.HttpContext
                .RequestServices
                .GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            // Get current user
            var userEmail = context.HttpContext.Session.GetString("UserEmail");
            var user = dbContext.Users.Include(u => u.Role)
                                        .ThenInclude(r => r.RolePermissions)
                                            .ThenInclude(rp => rp.Permission)
                                      .FirstOrDefault(user => user.Email == userEmail);

            if (user != null && user.Role != null)
            {
                // Get user permissions
                var userPermissions = user.Role.RolePermissions.Select(rp => rp.Permission.Name).ToList();
                // Check if user has all required permissions
                if (permissions.All(permission => userPermissions.Contains(permission)))
                    return;
                context.Result = new ForbidResult();
            } else
            {
                context.Result = new ForbidResult();
            }
        }
    }
}