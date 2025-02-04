using BookStore.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utilties.BusinessHelpers
{
    public class CurrentUser
    {
        private static readonly ClaimsPrincipal User;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public CurrentUser(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        public ApplicationUser GetUser()
        {
            return  _userManager.GetUserAsync(User).Result;
        }
    }
}
