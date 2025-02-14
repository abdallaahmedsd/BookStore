using BookStore.BusinessLogic.Services;
using BookStore.Models.ViewModels.Customer.OrderVM;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly OrderServices _orderService;

        public OrderController( OrderServices orderService)
        {
            _orderService = orderService;
        }

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            List<OrderListForCustomerViewModel> userOrderList = new List<OrderListForCustomerViewModel>();

            return View(userOrderList);
        }
    }
}
