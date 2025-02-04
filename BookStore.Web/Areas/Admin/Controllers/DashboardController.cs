using BookStore.Models.ViewModels;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<BestSellingBookDTO> books = (await BookServices.GetTopBestSellingBooksAsync(5)).ToList(); 
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
