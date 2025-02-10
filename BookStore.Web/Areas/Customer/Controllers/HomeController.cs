using BookStore.BusinessLogic.Services;
using BookStore.Models;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Customer;
using BookStore.Utilties.BusinessHelpers;
using BookStore.Web.Mappers;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BookStore.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookServices _bookService;
        private readonly ShoppingCartServices _shoppingCartService;
        private readonly SessionService _sessionService;

        public HomeController(ILogger<HomeController> logger, BookServices bookService, ShoppingCartServices shoppingCartService, SessionService sessionService)
        {
            _logger = logger;
            _bookService = bookService;
            _shoppingCartService = shoppingCartService; 
            _sessionService = sessionService;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel books = new HomeViewModel();

            books.LastAddedPublishedBooks = (await _bookService.GetLastAddedBooksAsync(5)).ToList();
            books.BestSellingBooks = (await _bookService.GetTopBestSellingBooksAsync(5)).ToList();

            #region Get the Cart items quantity from the db to show it

            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
            int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

            int cartQuantity = (int)(await _shoppingCartService.GetShoppingItemsCountByUserIdAsync(userId));

            _sessionService.SetCartQuantity(cartQuantity);

              #endregion

            return View(books);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}
