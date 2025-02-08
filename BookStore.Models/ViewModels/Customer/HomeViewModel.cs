using BookStore.Models.ViewModels.Customer.Book;

namespace BookStore.Models.ViewModels.Customer
{
    public class HomeViewModel
    {
        public List<BookHomeBestSellingViewModel> BestSellingBooks { get; set; }
        public List<BookHomeLastAddedViewModel> LastAddedPublishedBooks { get; set; }
    }
}
