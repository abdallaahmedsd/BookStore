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
        private readonly SessionService _sessionService;

        public BookController(BookServices bookService, ShoppingCartServices shoppingCartService,SessionService sessionService)
        {
            _bookService = bookService;
            _shoppingCartService = shoppingCartService;
            _sessionService = sessionService;
        }
       
        public async Task<ActionResult> Index(string? filterSearch)
        {
            BookListForCustomerViewModel allBooksViewModel = new BookListForCustomerViewModel
            {
                allBooks = (await _bookService.GetBookListAsync()).OrderBy(book => Guid.NewGuid()).ToList(),
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

                    ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                    int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);


                    var cartFromDb = await _shoppingCartService.GetShoppingCardByUserIDandBookIdAsync(userId,shoppingCartViewModel.BookId);

                    if (cartFromDb != null)
                    {
                        // update the quantity
                        cartFromDb.Quantity += shoppingCartViewModel.Quantity;
                        await _shoppingCartService.UpdateAsync(cartFromDb);
                    }
                    else
                    {
                        ShoppingCard shoppingCart = new ShoppingCard();

                        shoppingCartViewModel.UserId = userId;
                        Mapper.Map(shoppingCartViewModel, shoppingCart);

                        await _shoppingCartService.AddAsync(shoppingCart);
                    }

                    // update number of shopping cart items
                    await _SaveCartQuantityInSession(userId);

                    TempData["success"] = "تم إضافة المنتج للسلة بنجاح!";
                    return RedirectToAction(nameof(Index));
                }

                BookDetailsViewModel bookDetailsViewModel = await _bookService.GetBookDetailsByIdAsync(shoppingCartViewModel.BookId);
                BookDetailsForCustomerViewModel bookDetailsForCustomerViewModel = new();

                Mapper.Map(bookDetailsViewModel, bookDetailsForCustomerViewModel);
                return View("Details", bookDetailsForCustomerViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = "حصل خطأ أثناء إضافة المنتج إلى السلة";
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


    }
}
