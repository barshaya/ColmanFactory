using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_ColmanFactory1.Models;

namespace WebApplication_ColmanFactory1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                var productsOnSale = _context.Products.Where(p => p.IsOnSale == true).ToList();

                return View(productsOnSale);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult VisitUs()
        {

            return View();
        }
        public IActionResult About()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}