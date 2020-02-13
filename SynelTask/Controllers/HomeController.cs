using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SynelTask.Models;

namespace SynelTask.Controllers
{
    /// <summary>
    /// Default Controller of the application
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initialize a new instance of ILogger<HomeController>
        /// </summary>
        /// <param name="logger">A new instance of ILogger<HomeController></param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Index page of the Controller
        /// </summary>
        /// <returns>Returns Index view</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Error page that calls when exception occurs
        /// </summary>
        /// <returns>Return Exception page</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
