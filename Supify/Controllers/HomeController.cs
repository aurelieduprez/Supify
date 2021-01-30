using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Supify.Models;
using System.Diagnostics;

namespace Supify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Home/Manager");

            }
            else
            {
                return View();
            }
        }

      
        public IActionResult Manager()
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return Redirect("/Home");

            }
            else
            {
                return View();
            }

        }
        public IActionResult editPlaylist()
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return Redirect("/Home");

            }
            else
            {
                return View();
            }

        }
        public IActionResult deletePlaylist()
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return Redirect("/Home");

            }
            else
            {
                return View();
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
