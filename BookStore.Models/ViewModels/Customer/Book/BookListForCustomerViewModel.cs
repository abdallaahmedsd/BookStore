using BookStore.Models.ViewModels.Admin;
using BookStore.Models.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.ViewModels.Customer.Book
{
    public class BookListForCustomerViewModel
    {
        public  List<BookListViewModel> allBooks { get; set; }

        public string? filterSearch { get; set; } = "";

        //public int Id { get; set; }

        //[DisplayName("اسم المؤلف")]
        //public string AuthorName { get; set; }

        //[DisplayName("اسم الفئة")]
        //public string CategoryName { get; set; }

        //[DisplayName("الكتاب عنوان")]
        //public string Title { get; set; }

        //[DisplayName("وصف الكتاب")]
        //public string? Description { get; set; }

        //[DisplayName("سعر الكتاب")]
        //public decimal Price { get; set; }

        //[DisplayName("المعرف العالمى للكتاب (ISBA)")]
        //public string ISBA { get; set; }
        //public string? CoverImage { get; set; }

        //[DisplayName("تاريخ اصدار الكتاب")]
        //public DateTime PublicationDate { get; set; }
    }
}
