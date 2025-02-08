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
using Microsoft.AspNetCore.Hosting;
using BookStore.Models.ViewModels.Customer.Book;

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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(BookServices bookService, CategoryServices categoryServices, AuthorService authorService, BusinessLogic.Services.LanguageServices languageServices, IWebHostEnvironment webHostEnvironment)
        {
            _bookService = bookService;
            _categoryServices = categoryServices;
            _authorService = authorService;
            _languageServices = languageServices;
            _webHostEnvironment = webHostEnvironment;
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
                TempData["error"] = "حدث خطأ أثناء استرجاع الكتاب";
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
                TempData["error"] = "حدث خطأ أثناء استرجاع المعلومات";
                return View("Error");
            }
        }


        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddEditBookViewModel bookViewModel, IFormFile mainImage, List<IFormFile> files)
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

                    await _bookService.AddAsync(book);

                    #region Save Author iamges 
                    await _HandleBookImages(book.Id, bookViewModel, mainImage, files);

                    await _bookService.UpdateAsync(book);
                    #endregion


                    TempData["success"] = "تم إضافة الكتاب بنجاح!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["error"] = "حدث خطأ أثناء إضافة الكتاب.";
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
                TempData["error"] = "حدث خطأ أثناء استرجاع الكتاب للتعديل";
                return View("Error");
            }
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AddEditBookViewModel bookViewModel,IFormFile? mainImage, List<IFormFile>? files)
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


                    if (mainImage != null)
                    {
                        // if the main image chaneged
                        // remove the old main image from the database and from the hard desk as well

                        //var oldImageFromDb = await _unitOfWork.BookImage.GetAsync(x => x.BookId == id && x.IsMainImage);
                        //if (oldImageFromDb != null)
                        //{
                            //// Delete old image
                            string wwwRootPath = _webHostEnvironment.WebRootPath;

                            string fullPath = Path.Combine(wwwRootPath, bookModel.CoverImage.Trim('\\'));

                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                        //}

                    }

                    await _HandleBookImages(id, bookViewModel, mainImage, files);

                    Mapper.Map(bookViewModel, bookModel);

                    bookModel.UpdatedByUserID = userId;

                    await _bookService.UpdateAsync(bookModel);

                    //later: Save images in folder

                    TempData["success"] = "تم تحديث الكتاب بنجاح!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    bookViewModel.Mode = "Edit";

                    TempData["error"] = " حدث خطأ أثناء تعديل الكتاب";
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

        [HttpDelete("{id:int}")]
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
                TempData["error"] = "حدث خطأ أثناء استرجاع الكتاب للحذف";
                return View("Error");
            }
        }

        private async Task _HandleBookImages(int bookId, AddEditBookViewModel bookViewModel, IFormFile? mainImage, List<IFormFile>? files)
        {
            try
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string bookPath = @"uploads\images\books\book-" + bookId;
                string finalPath = Path.Combine(wwwRootPath, bookPath);

                if (!Directory.Exists(finalPath))
                    Directory.CreateDirectory(finalPath);

                // save the main image
                if (mainImage != null)
                {
                    await _CopayImage(mainImage, true);
                }

                // save others book images
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        await _CopayImage(file);
                    }
                }

                async Task _CopayImage(IFormFile file, bool isMainImage = false)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    //TbBookImage bookImage = new()
                    //{
                    //    ImageUrl = @"\" + bookPath + @"\" + fileName,
                    //    BookId = bookId,
                    //    IsMainImage = isMainImage
                    //};

                    //bookViewModel.BookImages.Add(bookImage);

                    bookViewModel.CoverImage = @"\" + bookPath + @"\" + fileName;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
