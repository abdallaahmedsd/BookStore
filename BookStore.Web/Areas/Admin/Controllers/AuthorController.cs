using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Admin;
using BookStore.Models.ViewModels.Admin.Book;
using BookStore.Web.Mappers;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AuthorController : Controller
    {
        private readonly AuthorService _authorService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthorController(AuthorService authorService, IWebHostEnvironment webHostEnvironment)
        {
            _authorService = authorService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            List<Author> authors = (await _authorService.GetAuthorsAsync()).ToList();

            //replace this with the listview

            return View(authors);
        }

        // GET: AuthorController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Author authorModel = (await _authorService.FindAsync(id));

            if (authorModel == null)
                return NotFound();

            AddEditAuthorViewModel authorViewModel = new();

            Mapper.Map(authorModel, authorViewModel);
            authorViewModel.Mode = "Details";

            return View(authorViewModel);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            try
            {
                AddEditAuthorViewModel addEditAuthorViewModel = new AddEditAuthorViewModel();

                return View(addEditAuthorViewModel);
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
        public async Task<ActionResult> Create(AddEditAuthorViewModel authorViewModel, IFormFile? authorProfileImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Author author = new Author();

                    ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                    int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                    Mapper.Map(authorViewModel, author);
                    author.CreatedBy = userId;

                    await _authorService._AddAsync(author);

                    #region Save Author iamges 

                    if(authorProfileImage != null)
                    {
                           await _HandleAuthorProfileImage(author.Id, authorViewModel, authorProfileImage);

                        author.ProfileImage = authorViewModel.ProfileImage;

                        await _authorService._UpdateAsync(author);
                    }
                 

                    #endregion

                    TempData["success"] = "Author created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["error"] = "An error occurred while creating the author.";
                    return View("Error");
                }
            }
            else
            {
                return View(authorViewModel);
            }
        }

        // GET: AuthorController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            if (id <= 0)
                return NotFound();

            try
            {
                Author authorModel = (await _authorService.FindAsync(id));

                if (authorModel == null)
                    return NotFound();

                AddEditAuthorViewModel authorViewModel = new();

                Mapper.Map(authorModel, authorViewModel);
                authorViewModel.Mode = "Edit";

                return View(authorViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while retrieving the author for editing.";
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AddEditAuthorViewModel authorViewModel, IFormFile? authorProfileImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                    int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                    Author? authorModel = await _authorService.FindAsync(id);

                    if (authorModel == null)
                        return NotFound();

                    #region Save Author iamges 
                    if (authorProfileImage != null)
                    {
                        // if the image chaneged
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        if(authorModel.ProfileImage != null)
                        {
                            string fullPath = Path.Combine(wwwRootPath, authorModel.ProfileImage.Trim('\\'));

                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                        }
                      
                    }

                    await _HandleAuthorProfileImage(id, authorViewModel, authorProfileImage);
                    #endregion

                    Mapper.Map(authorViewModel, authorModel);

                    await _authorService._UpdateAsync(authorModel);

                    //later: Save images in folder

                    TempData["success"] = "Author updated successfully!";
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
                // solve that not storing in the profileimage 

                authorViewModel.Mode = "Edit";
                return View(authorViewModel);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return NotFound();

            try
            {
                Author? authorModel = await _authorService.FindAsync(id);

                if (authorModel == null)
                    return NotFound();

                AddEditAuthorViewModel authorViewModel = new();

                Mapper.Map(authorModel, authorViewModel);
                authorViewModel.Mode = "Details";

                // handle it later
                return View(authorViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while retrieving the author for deletion.";
                return View("Error");
            }
        }

        private async Task _HandleAuthorProfileImage(int authorId, AddEditAuthorViewModel authorViewModel, IFormFile? mainImage )
        {
            try
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string authorPath = @"uploads\images\authors\author-" + authorId;
                string finalPath = Path.Combine(wwwRootPath, authorPath);

                if (!Directory.Exists(finalPath))
                    Directory.CreateDirectory(finalPath);

                // save the main image
                if (mainImage != null)
                {
                    await _CopayImage(mainImage, true);
                }

                async Task _CopayImage(IFormFile file, bool isMainImage = false)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }


                    authorViewModel.ProfileImage = @"\" + authorPath + @"\" + fileName;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
