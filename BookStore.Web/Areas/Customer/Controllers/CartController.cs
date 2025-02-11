using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.ViewModels.Customer.Book;
using BookStore.Models.ViewModels.Customer.Cart;
using BookStore.Models.ViewModels.Customer.OrderVM;
using BookStore.Web.Mappers;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace BookStore.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly ShoppingCartServices _shoppingCartService;
        private readonly CountryService _countryService;
        private readonly OrderServices _orderService;
        private readonly OrderItmeServices _orderItmeServices;
        private readonly ShippingServices _shippingServices;
        private readonly SessionService _sessionService;

        public readonly PaymentServices _paymentServices ;

        public CartController(ShoppingCartServices shoppingCartService,CountryService countryService, OrderServices orderService, OrderItmeServices orderItmeServices,ShippingServices shippingServices,PaymentServices paymentServices, SessionService sessionService)
        {
            _shoppingCartService = shoppingCartService;
            _countryService = countryService;
            _orderService = orderService;
            _orderItmeServices = orderItmeServices;
            _shippingServices = shippingServices;
            _paymentServices = paymentServices;
            _sessionService = sessionService;
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

                List<Country> allCountries = (await _countryService.GetCountriesAsync()).ToList();

                OrderSummaryViewModel CartViewModel = new OrderSummaryViewModel
                {
                    CartItems = CartItems,
                    Countries = allCountries,
                    OrderTotalAmount = _CalcOrderTotalAmount(CartItems),
                    EstimatedDelivery = DateTime.Now.AddDays(10)// will change acourding to the actual order
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
        public async Task<IActionResult> PlaceOrder(OrderSummaryViewModel orderViewModel)
        {
            try
            {
                List<OrderItemViewModel> CartItems;
                ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (ModelState.IsValid)
                {
                 
                    CartItems = (await _shoppingCartService.GetShoppingCartViewModelAsync(userId)).ToList();
                    // Add Order
                    Order order = new();

                    Mapper.Map( orderViewModel, order);
                    order.UserID = userId;
                    order.TotalAmoumt = orderViewModel.OrderTotalAmount;


                    await _orderService._AddAsync(order);

                    // Add OrderItems
                    foreach (var cartItem in CartItems)
                    {
                        OrderItem orderItem = new();
                        orderItem.OrderId = order.Id;

                        Mapper.Map(cartItem, orderItem);

                       await _orderItmeServices._AddAsync(orderItem);
                    }

                    // Clear the cart
                    await _shoppingCartService.DeleteCustomerItemsAsync(order.UserID);

                    await _SaveCartQuantityInSession(order.UserID);

                    // Add Shipping
                    Shipping shippng = new();

                    shippng.OrderID = order.Id;

                    Mapper.Map(orderViewModel, shippng);

                    await _shippingServices.AddAsync(shippng);

                    // Add Payment
                    Payment payment = new Payment();
                    Mapper.Map(orderViewModel, payment);

                    payment.OrderId = order.Id;
                    payment.UserId = userId;

                    await _paymentServices.AddAsync(payment);


                    TempData["success"] = "تم إضافة الطلب بنجاح!";

                    return RedirectToAction(nameof(OrderConfirmation), new { orderId = order.Id });
                }

                 CartItems = (await _shoppingCartService.GetShoppingCartViewModelAsync(userId)).ToList();
                List<Country> allCountries = (await _countryService.GetCountriesAsync()).ToList();

                OrderSummaryViewModel CartViewModel = new OrderSummaryViewModel
                {
                    CartItems = CartItems,
                    Countries = allCountries,
                    OrderTotalAmount = _CalcOrderTotalAmount(CartItems),
                    EstimatedDelivery = DateTime.Now // *****************
                };

                return View("Summary", orderViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = "حصل خطأ أثناء إضافة الطلب ";
                return View("Error");
            }
        }
        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            Order? order = await _orderService.GetOrderByIdAsync(orderId);

            if (order != null)
            {
            return View(orderId);
            }
            else
            {
                TempData["error"] = "حصل خطأ أثناء إضافة الطلب ";
                return View("Error");
            }
        }


        private async Task _SaveCartQuantityInSession(int userId)
        {
            try
            {
                int cartQuantity = (int)(await _shoppingCartService.GetShoppingItemsCountByUserIdAsync(userId));
                _sessionService.SetCartQuantity(cartQuantity);
            }
            catch (Exception ex)
            {
                TempData["error"] = "حصل خطأ أثناء استعادة كمية المنتجات اللتي في السلة";
                throw;
            }
        }

        private decimal _CalcOrderTotalAmount(List<OrderItemViewModel> CartItems)
        {
            return CartItems.Sum(item => item.SubTotal);
        }
    }
}
