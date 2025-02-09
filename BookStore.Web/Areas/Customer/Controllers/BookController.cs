using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.ViewModels.Customer.Book;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BookController : Controller
    {
        public readonly BookServices _bookService;

        public BookController(BookServices bookService)
        {
            _bookService = bookService;
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

        
        public async Task<ActionResult> Details(int id)
        {
            Book bookModel = await _bookService.FindAsync(id);
            return View(bookModel);
        }
    }
}
