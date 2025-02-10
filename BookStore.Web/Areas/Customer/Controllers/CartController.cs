using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Customer.Cart;
using BookStore.Models.ViewModels.Customer.OrderVM;
using BookStore.Web.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly ShoppingCartServices _shoppingCartService;

        public CartController(ShoppingCartServices shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
            int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

            List<OrderItemViewModel> CartItems = (await _shoppingCartService.GetShoppingCartViewModelAsync(userId)).ToList();


            CartListViewModel CartViewModel = new CartListViewModel
            {
                CartItems = CartItems,
                OrderTotalAmount = _CalcOrderTotalAmount(CartItems)
            };

            return View(CartViewModel);
        }

        public async Task<IActionResult> Summary()
        {
            try
            {
                ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                List<OrderItemViewModel> CartItems = (await _shoppingCartService.GetShoppingCartViewModelAsync(userId)).ToList();

                OrderSummaryViewModel CartViewModel = new OrderSummaryViewModel
                {
                    CartItems = CartItems,
                    OrderTotalAmount = _CalcOrderTotalAmount(CartItems),
                    EstimatedDelivery = DateTime.Now, // *****************
                    UserID= userId
                };

                return View(CartViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the summary. Please try again.";
                return View(new ShoppingCartViewModel()); // Return an empty view model
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> PlaceOrder(AddOrderViewModel orderViewModel)
        {

            // get all cartItems > create new order  > create orderItems and link them to the order 

            // clean the cart

            // all shipping info > add to the order

            // add user to the order

            // add status to the order

            //redirect the customer to a page or to the home page

            // activate the notification


            return RedirectToAction(nameof(Summary));


        }



        private decimal _CalcOrderTotalAmount(List<OrderItemViewModel> CartItems)
        {
            return CartItems.Sum(item => item.SubTotal);
        }
    }
}
