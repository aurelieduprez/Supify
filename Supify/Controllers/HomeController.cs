using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Supify.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Supify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //
        public IActionResult Index()
        {
            //if the user is connected, bring him to the /Home/manager view
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Home/Manager");

            }
            else //if not, let him on the index view
            {
                return View();
            }
        }

      
        public IActionResult Manager()
        {

            if (User.Identity.IsAuthenticated == false) //same system for each view, if not connected : you shall not pass
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


        public ActionResult onChange()
        {
            if (DarkTheme.isOn == true)
            {
                DarkTheme.isOn = false;
            }
            else
            {
                DarkTheme.isOn = true;
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }




}
