using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Customer.Cart;
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

            //List<ShoppingCard> shoppingCarts = (await _shoppingCartService.GetShoppingCardByUserIDAsync(userId)).ToList();

            //List<CartViewModel> shoppingCarts = (await _shoppingCartService.GetShoppingCardByUserIDAsync(userId)).ToList();

            List<CartViewModel> shoppingCarts = new();
            CartListViewModel shoppingCartViewModel = new CartListViewModel
            {
                CartItems = shoppingCarts,
                Order = new()
            };

            return View(shoppingCartViewModel);
        }
    }
}
