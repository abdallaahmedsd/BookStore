using BookStore.BusinessLogic.Services;
using BookStore.Models.ViewModels;
using BookStore.Models.ViewModels.Customer.Book;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public readonly BookServices _bookServices;

        public DashboardController(BookServices bookServices) {
            _bookServices = bookServices;
        }

        public async Task<IActionResult> Index()
        {
            List<BookHomeBestSellingViewModel> books = (await _bookServices.GetTopBestSellingBooksAsync(5)).ToList(); 
            return View(books);
        }

        public IActionResult Help()
        {
            return View();
        }
        public IActionResult Settings()
        {
            return View();
        }
    }
}
