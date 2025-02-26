using BookStore.BusinessLogic.Services;
using BookStore.Models;
using BookStore.Models.ViewModels.Customer;
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
        private readonly BookServices _bookService;

        public HomeController(ILogger<HomeController> logger, BookServices bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel books = new HomeViewModel();
            books.LastAddedPublishedBooks = (await _bookService.GetLastAddedBooksAsync(5)).ToList();
            books.BestSellingBooks = (await _bookService.GetTopBestSellingBooksAsync(5)).ToList();



            return View(books);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        
    }
}
