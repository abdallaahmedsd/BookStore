using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookStore.Models.ViewModels.Admin
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "مطلوب اسم التصنيف")]
        [StringLength(20, ErrorMessage = "اسم الـ التصنيف لا يمكن أن يتجاوز 50 حرف")]
        [DisplayName("اسم التصنيف")]
        public string Name { get; set; }
    }
}
