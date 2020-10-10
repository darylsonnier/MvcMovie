using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcMovie.Models;
using System.Diagnostics;

namespace MvcMovie.Controllers
{
    /// <summary>
    /// HomeController is the controller for the main home page of the site.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// The constructor takes in a ILogger
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// The Index method returns the view for the Home page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;  // Total number of items in cart
            return View();
        }

        /// <summary>
        /// The Privacy method returns the view for the Privacy page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            return View();
        }

        /// <summary>
        /// The Error method returns an error page in the event that the controller cannot process an action.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
