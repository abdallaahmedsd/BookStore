using BookStore.DataAccess.EntityFrameworkCore.Data;
using BookStore.Models.Identity;
using BookStore.Utilties;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _context ;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DbInitializer(AppDbContext context, UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager) 
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if(_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }

                if (!_roleManager.RoleExistsAsync( AppUserRoles.RoleAdmin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new ApplicationRole() { Name = AppUserRoles.RoleAdmin }).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new ApplicationRole() { Name = AppUserRoles.RoleCustomer }).GetAwaiter().GetResult();
                }

                if (!_context.ApplicationUsers.Any())
                {
                    ApplicationUser user = new();
                    user.FirstName = "ARAMM";
                    user.LastName = "Winners";
                    user.Email = "admin@aramm.com";
                    user.UserName = "admin@aramm.com";
                    user.PasswordHash = "Aramm123@";

                    var result =  _userManager.CreateAsync(user, "Aramm123@").GetAwaiter().GetResult();

                    if(result.Succeeded)
                    {
                        _userManager.AddToRoleAsync(user, AppUserRoles.RoleAdmin).GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Something got wrong while initializing the database: {ex.Message}");
            }
        }
    }
}

