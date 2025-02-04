using System.ComponentModel;

namespace BookStore.Models.ViewModels.Admin.Book
{
    public class AddEditBookViewModel
    {
        public int Id { get; set; }

        [DisplayName("اختيار المؤلف")]
        public int AuthorID { get; set; }

        [DisplayName("اختيار الفئة")]
        public int CategoryID { get; set; }

        [DisplayName("اختيار لغه الكتاب")]
        public int LanguageID { get; set; }

        [DisplayName("الكتاب عنوان")]
        public string Title { get; set; }

        [DisplayName("سعر الكتاب")]
        public decimal Price { get; set; }

        [DisplayName("نبذة عن الكتاب")]
        public string? Description { get; set; }

        [DisplayName("المعرف العالمى للكتاب (ISBA)")]
        public string ISBA { get; set; }

        [DisplayName("تاريخ اصدار الكتاب")]
        public DateTime PublicationDate { get; set; }
        public string? CoverImage { get; set; }

        [DisplayName("عدد النسخ")]
        public string Quantity { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<AuthorViewModel> Authors { get; set; }

    }
}
