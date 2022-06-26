using DemoSeguridad.Data;
using DemoSeguridad.Models;
using DemoSeguridad.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoSeguridad.Authorization
{
    public class AuthorizeFilter : IAuthorizationFilter
    {
        private readonly ICollection<string> permissions;

        public AuthorizeFilter(string[] permissions = null)
        {
            this.permissions = permissions ?? Array.Empty<string>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Get current user
            var user = context.HttpContext.Session.GetObjectFromJson<UserViewModel>("User");

            if (user != null)
            {
                if(user.Role != null)
                {
                    // Get user permissions
                    var userPermissions = user.Role.Permissions.Select(p => p.Name).ToList();
                    // Check if user has all required permissions
                    if (permissions.All(permission => userPermissions.Contains(permission)))
                        return;
                }
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "UnAuthorized" }
                });
            } else // If not logged in, redirect to Login
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "LogIn" },
                    { "action", "LogIn" }
                });
            }
        }
    }
}