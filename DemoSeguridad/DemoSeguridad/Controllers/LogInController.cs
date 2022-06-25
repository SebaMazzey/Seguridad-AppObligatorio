using DemoSeguridad.Data;
using DemoSeguridad.Data.Entities;
using DemoSeguridad.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
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

        public LogInController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
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

                var user = _context.Users.FirstOrDefault(user => user.Email == email);
                if (user != null)
                {
                    var hashedPass = Encoding.Default.GetString(user.HashPassword);
                    var a = user.FirstName;
                    if (BCrypt.Net.BCrypt.Verify(password + user.HashSalt, hashedPass))
                    {
                        HttpContext.Session.SetString("Email", user.Email);
                        HttpContext.Session.SetString("Rol", user.RoleName);
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

                HttpContext.Session.SetString("Email", newUser.Email);
                HttpContext.Session.SetString("Rol", defaultRole);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private static string GenerateSalt()
        {
            var random = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[SALT_LENGHT];

            random.GetNonZeroBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        private static Byte[] HashPassword(string password, string salt)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password + salt);
            var passwordBytes = Encoding.Default.GetBytes(passwordHash);
            return passwordBytes;
        }
    }
}
