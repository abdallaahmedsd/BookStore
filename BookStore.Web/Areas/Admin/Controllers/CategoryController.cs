using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Admin.Book;
using BookStore.Models.ViewModels.Admin;
using BookStore.Web.Mappers;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        public readonly BookServices _bookService;
        private readonly CategoryServices _categoryServices;
        private readonly AuthorService _authorService;
        private readonly BusinessLogic.Services.LanguageServices _languageServices;

        public CategoryController(BookServices bookService, CategoryServices categoryServices, AuthorService authorService, BusinessLogic.Services.LanguageServices languageServices)
        {
            _bookService = bookService;
            _categoryServices = categoryServices;
            _authorService = authorService;
            _languageServices = languageServices;
        }

        // GET: CategoriesController
        public async Task<ActionResult> Index()
        {
            return View((await _categoryServices.GetAllAsync()).ToList());
        }

        // GET: CategoriesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {

                Category categoryModel = await _categoryServices.FindAsync(id);

                if (categoryModel == null)
                    return NotFound();


                CategoryViewModel categoryViewModel = new CategoryViewModel();

                Mapper.Map(categoryModel, categoryViewModel);


                return View(categoryViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while retrieving the category.";
                return View("Error");
            }
        }

        // GET: CategoriesController/Create
        public async Task<ActionResult> Create()
        {
            try
            {
                CategoryViewModel categoryViewModel = new CategoryViewModel();

                return View(categoryViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while retrieving the category.";
                return View("Error");
            }
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                    int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                    Category categoryModel = new Category();

                    Mapper.Map(categoryViewModel, categoryModel);

                    categoryModel.CreatedBy = userId;

                    await _categoryServices.AddAsync(categoryModel);


                    TempData["success"] = "Category created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["error"] = "An error occurred while creating the Category.";
                    return View("Error");
                }
            }
            else
            {
                return View(categoryViewModel);
            }
        }

        // GET: CategoriesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id <= 0)
                return NotFound();

            try
            {
                Category categoryModel = await _categoryServices.FindAsync(id);

                if (categoryModel == null)
                    return NotFound();

                CategoryViewModel categoryViewModel = new CategoryViewModel();

                Mapper.Map(categoryModel, categoryViewModel);

                return View(categoryViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while retrieving the category for editing.";
                return View("Error");
            }
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Category categoryModel = await _categoryServices.FindAsync(id);

                    if (categoryModel == null)
                        return NotFound();



                    Mapper.Map(categoryViewModel, categoryModel);


                    await _categoryServices.UpdateAsync(categoryModel);

                    //later: Save images in folder

                    TempData["success"] = "Category updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    // Log exception (ex) here
                    TempData["error"] = "An error occurred while updating the Category.";
                    return View("Error");
                }
            }
            else
            {
               
                return View(categoryViewModel);
            }
        }


        // POST: CategoriesController/Delete/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {


            try
            {
                Category categoryModel = await _categoryServices.FindAsync(id);

                if (categoryModel == null)
                    return NotFound();

                await _categoryServices.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log exception (ex) here
                TempData["error"] = "An error occurred while retrieving the category for deletion.";
                return View("Error");
            }
        }
    }
}
