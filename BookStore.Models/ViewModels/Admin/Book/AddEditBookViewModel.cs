using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.ViewModels.Admin.Book
{
    public class AddEditBookViewModel
    {
        public int Id { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "اسم المؤلف مطلوب")]
        [Required(ErrorMessage = "اسم المؤلف مطلوب")]
        [DisplayName("اختيار المؤلف")]
        public int AuthorID { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "يجب اختيار فئة")]
        [Required(ErrorMessage = "يجب اختيار فئة")]
        [DisplayName("اختيار الفئة")]
        public int CategoryID { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "يجب اختيار لغه")]
        [Required(ErrorMessage = "يجب اختيار لغه")]
        [DisplayName("اختيار لغه الكتاب")]
        public int LanguageID { get; set; }


        [Required(ErrorMessage = "العنوان مطلوب")]
        [StringLength(100, ErrorMessage = "العنوان لا يمكن أن يتجاوز 100 حرف")]
        [DisplayName("عنوان الكتاب")]
        public string Title { get; set; }

        [Range(0.0001, double.MaxValue, ErrorMessage = "يجب أن يكون السعر رقماً موجباً")]
        [Required(ErrorMessage = "السعر مطلوب")]
        [DisplayName("سعر الكتاب")]
        public decimal Price { get; set; }

        [DisplayName("وصف الكتاب")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "رقم الـ ISBN مطلوب")]
        [StringLength(20, ErrorMessage = "رقم الـ ISBN لا يمكن أن يتجاوز 20 حرف")]
        [DisplayName("المعرف العالمى للكتاب (ISBA)")]
        public string ISBA { get; set; }

        
        [Required(ErrorMessage = "تاريخ اصدار الكتاب مطلوب")]
        [DisplayName("تاريخ اصدار الكتاب")]
        public DateTime PublicationDate { get; set; }
        public string? CoverImage { get; set; }


        public string Mode { get; set; } = "Create";

        //[ValidateNever]
        public List<CategoryViewModel> Categories { get; set; } = [];
        //[ValidateNever]
        public List<AuthorViewModel> Authors { get; set; } = [];
        //[ValidateNever]
        public List<LanguageViewModel> Languages { get; set; } = [];

    }
}
