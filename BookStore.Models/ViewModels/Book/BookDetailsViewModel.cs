using System.ComponentModel;

namespace BookStore.Models.ViewModels.Book;

    public class BookDetailsViewModel
{
        public int Id { get; set; }

        [DisplayName("اسم المؤلف")]
        public string AuthorName { get; set; }

        [DisplayName("اسم الفئة")]
        public string CategoryName { get; set; }

        [DisplayName("لغه الكتاب")]
        public string LanguageName { get; set; }

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

        [DisplayName("عدد المبيعات")]
        public int TotalSellingQuantity { get; set; } 

}

