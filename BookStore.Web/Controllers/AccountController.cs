using BookStore.Models.Identity;
using BookStore.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class AccountController : Controller
    {
        //private readonly UserManager<ApplicationUser> userManager;

        //public AccountController(UserManager<ApplicationUser> userManager)
        //{
        //    this.userManager = userManager;
        //}

        [HttpGet]
       
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<IActionResult> Register(RegisterUserViewModel UserVM)
        {
            // If valid
            if(ModelState.IsValid)
            {

                // Map VM -> M
                ApplicationUser userModel = new ApplicationUser()
                {
                    FirstName = UserVM.FirsName,
                    LastName = UserVM.LastName,
                    Email = UserVM.Email,
                    PasswordHash = UserVM.Password
                };

                //IdentityResult result =  await userManager.CreateAsync(userModel);

                //if(result.Succeeded)
                //{
                //    //Save in DB
                //    //Create Cookie

                //    return RedirectToAction("Index", "Home");
                //}
                //else
                //{
                //    foreach(var error in result.Errors)
                //    {
                //        ModelState.AddModelError("Password",$"{error}");
                //    }
                //}
            }



            return View(UserVM);
        }
    }
}
