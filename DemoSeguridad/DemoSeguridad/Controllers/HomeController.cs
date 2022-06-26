using DemoSeguridad.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoSeguridad.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

        public IActionResult UnAuthorized()
        {
            return View();
        }
    }
}
