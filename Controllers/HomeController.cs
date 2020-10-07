using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MvcMovie.Models;

namespace MvcMovie.Controllers
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
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;  // Total number of items in cart
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
