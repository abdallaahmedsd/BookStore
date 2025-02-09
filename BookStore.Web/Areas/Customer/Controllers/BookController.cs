using BookStore.BusinessLogic.Services;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.ViewModels.Customer.Book;
using BookStore.Models.ViewModels.Customer.Cart;
using BookStore.Utilties;
using BookStore.Web.Mappers;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BookController : Controller
    {
        public readonly BookServices _bookService;
        private readonly ShoppingCartServices _shoppingCartService;

        public BookController(BookServices bookService, ShoppingCartServices shoppingCartService)
        {
            _bookService = bookService;
            _shoppingCartService = shoppingCartService;
        }
       
        public async Task<ActionResult> Index(string? filterSearch)
        {

            BookListForCustomerViewModel allBooksViewModel = new BookListForCustomerViewModel
            {
                allBooks = (await _bookService.GetBookListAsync()).ToList(),
                filterSearch = filterSearch
            };

            return View(allBooksViewModel);
        }


        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            BookDetailsViewModel bookDetailsViewModel = await _bookService.GetBookDetailsByIdAsync(id);

            BookDetailsForCustomerViewModel bookDetailsForCustomerViewModel = new();

            Mapper.Map(bookDetailsViewModel, bookDetailsForCustomerViewModel);

            return View(bookDetailsForCustomerViewModel);
        }

        [HttpPost]
        [Authorize]
        [ActionName("Details")]
        public async Task<IActionResult> AddToCart(AddToCartViewModel shoppingCartViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //List<ShoppingCard> shoppingCartItems = (await _shoppingCartService.GetShoppingCardByUserIDAsync(userId)).ToList();

                    ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                    int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);


                    var cartFromDb = (await _shoppingCartService.GetShoppingCardByUserIDAsync(userId)).ToList()[0];
                    //var cartFromDb = await _unitOfWork.ShoppingCart.GetAsync(x => x.UserId == userId && x.BookId == shoppingCartViewModel.BookId);

                    if (cartFromDb != null)
                    {
                        // update the quantity
                        cartFromDb.Quantity += shoppingCartViewModel.Quantity;
                        //_shoppingCartService.UpdateAsync(cartFromDb);
                    }
                    else
                    {
                        ShoppingCard shoppingCart = new ShoppingCard();

                        shoppingCartViewModel.UserId = userId;
                        Mapper.Map(shoppingCartViewModel, shoppingCart);

                        await _shoppingCartService.AddAsync(shoppingCart);
                    }

                    // update number of shopping cart items
                    _SaveCartQuantityInSession(userId);

                    TempData["success"] = "تم إضافة المنتج للسلة بنجاح!";
                    return RedirectToAction(nameof(Index));
                }

                return View("Details", new { shoppingCartViewModel.BookId });
            }
            catch (Exception ex)
            {
                TempData["error"] = "حصل خطأ أثناء إضافة المنتج إلى السلة";
                return View("Error");
            }
        }

        private async void _SaveCartQuantityInSession(int userId)
        {
            try
            {
               int cartQuantity = (await _shoppingCartService.GetShoppingCardByUserIDAsync(userId)).ToList().Count();

                HttpContext.Session.SetInt32(SessionHelper.SessionCart, cartQuantity);
            }
            catch (Exception ex)
            {
                TempData["error"] = "حصل خطأ أثناء استعادة كمية المنتجات اللتي في السلة";
                throw;
            }
        }


    }
}
