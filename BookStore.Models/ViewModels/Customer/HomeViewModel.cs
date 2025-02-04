namespace BookStore.Models.ViewModels.Customer
{
    public class HomeViewModel
    {
        public List<BestSellingBookDTO> BestSellingBooks { get; set; }
        public List<RecentlyPublishedBookDTO> RecentlyPublishedBooks { get; set; }
    }
}
