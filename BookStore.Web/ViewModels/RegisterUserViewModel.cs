using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.ViewModels
{
    public class RegisterUserViewModel
    {

        public string FirsName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        //[UniqueUsername] Custom
        //  public string UserName { get; set; }= null!;
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
