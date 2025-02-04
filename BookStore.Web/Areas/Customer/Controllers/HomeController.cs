using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Utilties.BusinessHelpers;
using BookstoreBackend.BLL.Services;
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

        public async Task<IActionResult> Index()
        {
            List<BestSellingBookDTO> books = (await BookServices.GetTopBestSellingBooksAsync(5)).ToList();
            return View(books);
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
