using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
