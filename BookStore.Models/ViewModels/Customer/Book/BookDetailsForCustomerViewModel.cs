using BookStore.Models.ViewModels.Admin;
using BookStore.Models.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.ViewModels.Customer.Book
{
    public class BookDetailsForCustomerViewModel
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

        [DisplayName("الكمية")]
        public int Quantity { get; set; }
    }
}
