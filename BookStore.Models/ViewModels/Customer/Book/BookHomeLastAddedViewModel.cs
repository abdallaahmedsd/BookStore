using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Customer.Book
{
    internal class BookHomeLastAddedViewModel
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

        [DisplayName("تاريخ اصدار الكتاب")]
        public DateTime PublicationDate { get; set;}
    }
}
