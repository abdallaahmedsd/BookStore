namespace BookStore.Models.ViewModels.Admin
{
    public class AddEditAuthorViewModel
    {
        public int Id { get; set; }

        public string Bio { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int NationalityID { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; }
        public string? ProfileImage { get; set; }
        public string Mode { get; set; } = "Create";

        
    }
}
