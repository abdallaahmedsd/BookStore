using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookStore.Models.Identity;
using BookStore.Models.ViewModels;
using BookStore.Models.ViewModels.Admin;
using BookStore.Models.ViewModels.Admin.Book;
using BookStore.Web.Mappers;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        public readonly BookServices _bookService;

        public BookController(BookServices bookService)
        {
            _bookService = bookService;
        }

        // GET: BookController
        public async Task<ActionResult> Index()
        {
            List<BookDTO> books = (await BookServices.GetAllAsync()).ToList();

            return View(books);
        }

        // GET: BookController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await BookServices.FindAsync(id));
        }

        // GET: BookController/Create
        public async Task<ActionResult> Create()
        {
            try
            {

                //List<AuthorViewModel> authors = (await AuthorServices.GetAllAsync())
                //   .Select(x => new AuthorViewModel
                //   {
                //       Id = x.Id,
                //       Name = x.Name
                //   }).ToList();

                List<CategoryViewModel> categories = (await CategoryServices.GetAllAsync())
                    .Select(x => new CategoryViewModel
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList();

                var bookViewModel = new AddEditBookViewModel
                {
                    Categories = categories
                     //    Authors = authors
                 };



                return View(bookViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while retrieving the categories.";
                return View("Error");
            }
        }


        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddEditBookViewModel bookViewModel)
        {
            try
            {
                ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                Book book = new Book();

                Mapper.Map(bookViewModel, book);

                book.UpdatedByUserID = userId;

                // _bookService.Add(book);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = "An error occurred while adding a new book.";
                return View("Error");
            }
        }






        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookController/Edit/5
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

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookController/Delete/5
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
