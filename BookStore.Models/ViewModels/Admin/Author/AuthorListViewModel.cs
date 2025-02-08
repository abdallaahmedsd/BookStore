namespace BookStore.Models.ViewModels.Admin
{
    public class AuthorListViewModel
    {
        public int Id { get; set; }

        public string Bio { get; set; }
       
        public int CreatedBy { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string? ProfileImage { get; set; }
    }
}
