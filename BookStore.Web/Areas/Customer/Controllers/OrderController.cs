using BookStore.BusinessLogic.Services;
using BookStore.Models.ViewModels.Customer.Cart;
using BookStore.Models.ViewModels.Customer.OrderVM;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            try
            {

                ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                List<OrderListForCustomerViewModel> userOrderList = (await _orderService.GetOrderListForCustomerViewModelByUserId(userId)).ToList();

                return View(userOrderList);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while retrieve the orders for user. Please try again.";
                return View("Error");

            }
        }
    }
}
