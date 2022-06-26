using DemoSeguridad.Data;
using DemoSeguridad.Data.Entities;
using DemoSeguridad.Models;
using DemoSeguridad.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DemoSeguridad.Controllers
{
    public class LogInController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly string defaultRole = "Lector";
        private const int SALT_LENGHT = 30;
        private readonly string pepper;

        public LogInController(ILogger<HomeController> logger, ApplicationDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            pepper = configuration.GetValue<string>("Pepper");
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(UserLoginModel loginModel)
        {
            try
            {
                // Sanitizar variables aca
                var email = loginModel.Email;
                var password = loginModel.Password;

                if (!ModelState.IsValid)
                    return View(loginModel);

                // Retrieve user with roles and permission
                var user = _context.Users.Include(u => u.Role)
                                            .ThenInclude(r => r.RolePermissions)
                                                .ThenInclude(rp => rp.Permission)
                                         .FirstOrDefault(user => user.Email == email);
                if (user != null)
                {
                    var hashedPass = Encoding.Default.GetString(user.HashPassword);
                    var a = user.FirstName;
                    if (BCrypt.Net.BCrypt.Verify(password + user.HashSalt + pepper, hashedPass))
                    {
                        HttpContext.Session.SetObjectAsJson("User", ModelConverter.GetUserViewModel(user));
                        return Redirect("/book/bookList");
                    }
                }
                ModelState.AddModelError("", "Email o contraseña incorrectos, vuelta a intentar.");
                return View(loginModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Se produjo un error inesperado, intente mas tarde");
                return View(loginModel);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterModel registerModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(registerModel);
                // Sanitizar las variables aca
                var firstName = registerModel.FirstName;
                var lastName = registerModel.LastName;
                var email = registerModel.Email;
                var password = registerModel.Password;

                // Check if email already in use
                if (_context.Users.Any(user => user.Email == email))
                {
                    ModelState.AddModelError("", "El email ingresado ya esta en uso");
                    return View(registerModel);
                }

                // Generate Salt and hashPassword with BCrypt
                var salt = GenerateSalt();
                var passwordBytes = HashPassword(password, salt);

                // Create and add user (With default role)
                User newUser = new()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    HashPassword = passwordBytes,
                    HashSalt = salt,
                    RoleName = defaultRole
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                // Get role with permissions to store in session
                var role = _context.Roles.Include(r => r.RolePermissions).ThenInclude(rp => rp.Permission).FirstOrDefault(r => r.Name == defaultRole);
                newUser.Role = role;

                HttpContext.Session.SetObjectAsJson("User", ModelConverter.GetUserViewModel(newUser));
                return Redirect("/book/bookList");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Se produjo un error inesperado, intente mas tarde");
                return View(registerModel);
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return Redirect("/home/index");
        }

        private static string GenerateSalt()
        {
            var random = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[SALT_LENGHT];

            random.GetNonZeroBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        private Byte[] HashPassword(string password, string salt)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password + salt + pepper);
            var passwordBytes = Encoding.Default.GetBytes(passwordHash);
            return passwordBytes;
        }
    }
}
