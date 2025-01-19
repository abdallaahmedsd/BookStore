using Microsoft.AspNetCore.Identity;

namespace BookStore.Models.Identity
{
	public class ApplicationUser : IdentityUser<int>
	{
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
