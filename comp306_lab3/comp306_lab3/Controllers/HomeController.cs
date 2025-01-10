using comp306_lab3.Models;
using comp306_lab3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace comp306_lab3.Controllers
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
            return View("../Login/Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Register()
        {

            return View("../Account/Register");
        }

        public IActionResult ProcessRegister(AccountRegisterModel model)
        {
            SecurityService securityService = new SecurityService();
            if(!(securityService.InsertNewUser(model)))
            {
                Console.WriteLine("Failed to insert user");
                return View("../Account/Register");
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult ProcessLogin(UserModel userModel)
        {
            SecurityService securityService = new SecurityService();
            UserModel foundUser = securityService.isValid(userModel);
            if(foundUser != null)
            {
                HttpContext.Session.SetString("UserModel", JsonConvert.SerializeObject(foundUser));
                // Console.WriteLine("Debugging - TempData['UserModel']:" + TempData["UserModel"].ToString());

                return RedirectToAction("MovieListing", "Movie");
            }
            else
            {
                return View("../Login/LoginFailure", userModel);
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
