using BookStore.BusinessLogic.Services;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.ViewModels.Customer.Book;
using BookStore.Models.ViewModels.Customer.Cart;
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

        //[HttpPost]
        //[Authorize]
        //[ActionName("Details")]
        //public async Task<IActionResult> AddToCart(AddToCartViewModel viewModel)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
        //            int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);


        //            //ShoppingCard? card = await _shoppingCartService.FindByUserId(userId);
        //            ShoppingCard? card = new()

        //            var cartFromDb = await _unitOfWork.ShoppingCart.GetAsync(x => x.UserId == userId && x.BookId == viewModel.BookId);

        //            if (cartFromDb != null)
        //            {
        //                // update the quantity
        //                cartFromDb.Quantity += viewModel.Quantity;
        //                _unitOfWork.ShoppingCart.Update(cartFromDb);
        //            }
        //            else
        //            {
        //                // create new 
        //                var shoppingCart = new TbShoppingCart
        //                {
        //                    UserId = userId,
        //                    Quantity = viewModel.Quantity,
        //                    BookId = viewModel.BookId
        //                };

        //                await _unitOfWork.ShoppingCart.AddAsync(shoppingCart);
        //            }

        //            await _unitOfWork.SaveAsync();

        //            // update number of shopping cart items
        //            _SaveCartQuantityInSession(userId);

        //            TempData["success"] = "Item added to the cart successfully!";
        //            return RedirectToAction(nameof(Index));
        //        }

        //        return View("Details", new { viewModel.BookId });
        //    }
        //    catch (Exception ex)
        //    {
        //        // _logger.LogError(ex, "An error occurred while adding the book to the cart.");
        //        TempData["error"] = "An error occurred while adding the book to the cart.";
        //        return View("Error");
        //    }
        //}




    }
}
