using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookStore.Models.Identity;
using BookStore.Models.ViewModels;
using BookStore.Models.ViewModels.Admin;
using BookStore.Models.ViewModels.Admin.Book;
using BookStore.Models.ViewModels.Book;
using BookStore.Web.Mappers;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Utilties;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = AppUserRoles.RoleAdmin)]
    public class BookController : Controller
    {
        public readonly BookServices _bookService;
        private readonly CategoryServices _categoryServices;
        private readonly AuthorService _authorService;
        private readonly BusinessLogic.Services.LanguageServices _languageServices;

        public BookController(BookServices bookService, CategoryServices categoryServices, AuthorService authorService, BusinessLogic.Services.LanguageServices languageServices)
        {
            _bookService = bookService;
            _categoryServices = categoryServices;
            _authorService = authorService;
            _languageServices = languageServices;
        }

        // GET: BookController
        public async Task<ActionResult> Index()
        {
            List<BookListViewModel> books = (await _bookService.GetBookListAsync()).ToList();

            return View(books);
        }

        // GET: BookController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                //BookDetailsViewModel bookViewModel = await _bookService.GetBookDetailsByIdAsync(id);

                Book bookModel = (await _bookService.FindAsync(id));

            if (bookModel == null)
                return NotFound();

            List<AuthorViewModel> authors = (await _authorService.GetAuthorViewModelAsync()).ToList();

            List<CategoryViewModel> categories = (await _categoryServices.GetAllCategoryViewModelAsync()).ToList();

            List<LanguageViewModel> languages = (await _languageServices.GetAllLanguageViewModelAsync()).ToList();

            var bookViewModel = new AddEditBookViewModel
            {
                Categories = categories,
                Authors = authors,
                Languages = languages
            };

            Mapper.Map(bookModel, bookViewModel);

            bookViewModel.Mode = "Details";

            return View(bookViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while retrieving the book.";
                return View("Error");
            }
        }

        // GET: BookController/Create
        public async Task<ActionResult> Create()
        {
            try
            {
                List<AuthorViewModel> authors = (await _authorService.GetAuthorViewModelAsync()).ToList();

                List<CategoryViewModel> categories = (await _categoryServices.GetAllCategoryViewModelAsync()).ToList();

                List<LanguageViewModel> languages = (await _languageServices.GetAllLanguageViewModelAsync()).ToList();

                var bookViewModel = new AddEditBookViewModel
                {
                    Categories = categories,
                    Authors = authors,
                    Languages = languages
                };


                return View(bookViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while retrieving the data.";
                return View("Error");
            }
        }


        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddEditBookViewModel bookViewModel)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                    int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                    Book book = new Book();

                    Mapper.Map(bookViewModel, book);

                    book.UpdatedByUserID = userId;

                    _bookService.AddAsync(book);

                    //later: Save images in folder

                    TempData["success"] = "Book created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["error"] = "An error occurred while creating the book.";
                    return View("Error");
                }
            }
            else
            {
                List<AuthorViewModel> authors = (await _authorService.GetAuthorViewModelAsync()).ToList();

                List<CategoryViewModel> categories = (await _categoryServices.GetAllCategoryViewModelAsync()).ToList();

                List<LanguageViewModel> languages = (await _languageServices.GetAllLanguageViewModelAsync()).ToList();

                bookViewModel = new AddEditBookViewModel
                {
                    Categories = categories,
                    Authors = authors,
                    Languages = languages
                };
                return View(bookViewModel);
            }
        }



        // GET: BookController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id <= 0)
                return NotFound();

            try
            {
                Book bookModel = (await _bookService.FindAsync(id));

                if (bookModel == null)
                    return NotFound();

                List<AuthorViewModel> authors = (await _authorService.GetAuthorViewModelAsync()).ToList();

                List<CategoryViewModel> categories = (await _categoryServices.GetAllCategoryViewModelAsync()).ToList();

                List<LanguageViewModel> languages = (await _languageServices.GetAllLanguageViewModelAsync()).ToList();

                var bookViewModel = new AddEditBookViewModel
                {
                    Categories = categories,
                    Authors = authors,
                    Languages = languages
                };

                Mapper.Map(bookModel, bookViewModel);
                bookViewModel.Mode = "Edit";

                return View(bookViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while retrieving the book for editing.";
                return View("Error");
            }
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AddEditBookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                    int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                    var bookModel = await _bookService.FindAsync(id);

                    if (bookModel == null)
                        return NotFound();

                    Mapper.Map(bookViewModel, bookModel);

                    bookModel.UpdatedByUserID = userId;

                    await _bookService.UpdateAsync(bookModel);

                    //later: Save images in folder

                    TempData["success"] = "Book updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    // Log exception (ex) here
                    TempData["error"] = "An error occurred while updating the book.";
                    return View("Error");
                }
            }
            else
            {
                List<AuthorViewModel> authors = (await _authorService.GetAuthorViewModelAsync()).ToList();

                List<CategoryViewModel> categories = (await _categoryServices.GetAllCategoryViewModelAsync()).ToList();

                List<LanguageViewModel> languages = (await _languageServices.GetAllLanguageViewModelAsync()).ToList();

                bookViewModel = new AddEditBookViewModel
                {
                    Categories = categories,
                    Authors = authors,
                    Languages = languages
                };
                return View(bookViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return NotFound();

            try
            {
                Book book = await _bookService.FindAsync(id);

                if (book == null)
                    return NotFound();

                await _bookService.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log exception (ex) here
                TempData["error"] = "An error occurred while retrieving the book for deletion.";
                return View("Error");
            }
        }

    }
}
