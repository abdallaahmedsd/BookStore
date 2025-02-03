using BookStore.Models;
using BookStore.Utilties.BusinessHelpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Main()
        //{
        //    return View();
        //}

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });


        public IActionResult AdminDashboard() => View();

        
    }
}
