using System.ComponentModel;

namespace BookStore.Models.ViewModels.Book
{
    public class BookListViewModel
    {
        public int Id { get; set; }

        [DisplayName("اسم المؤلف")]
        public string AuthorName { get; set; }

        [DisplayName("اسم الفئة")]
        public string CategoryName { get; set; }

        [DisplayName("الكتاب عنوان")]
        public string Title { get; set; }

        [DisplayName("سعر الكتاب")]
        public decimal Price { get; set; }
        public string? CoverImage { get; set; }
    }
}
