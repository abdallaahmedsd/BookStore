using BookStore.BusinessLogic.Services;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Customer.Cart;
using BookStore.Utilties.BusinessHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {

        private readonly ShoppingCartServices _shoppingCartService;

        public CartController( ShoppingCartServices shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }



        // GET: ShoppingCard
        public async Task<IActionResult> Index()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
            int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

            List<ShoppingCard> shoppingCarts = (await _shoppingCartService.GetShoppingCardByUserIDAsync(userId)).ToList();

            ShoppingCartViewModel shoppingCartViewModel = new ShoppingCartViewModel
            {
                shoppingCartItems = (await _shoppingCartService.GetShoppingCardByUserIDAsync(userId)).ToList(),
                Order = new()
            };

            return View(shoppingCartViewModel);
        }

        // GET: ShoppingCard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShoppingCard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingCard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShoppingCard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingCard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShoppingCard/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
