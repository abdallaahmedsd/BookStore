using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace BookStore.Models.ViewModels.Admin
{
    public class AddEditAuthorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "السيرة الذاتية مطلوبة")]
        [DisplayName("السيرة الذاتية")]
        public string Bio { get; set; }

        [Required(ErrorMessage = "الاسم الأول مطلوب")]
        [DisplayName("الاسم الأول ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "الاسم الأخير مطلوب")]
        [DisplayName("الاسم الأخير")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "الرقم الوطني مطلوب")]
        [DisplayName("الرقم الوطني")]
        public int NationalityID { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "الرقم يجب أن يتألف من 11 خانة حصرا")]
        [DisplayName("الرقم (اختياري)")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "الايميل مطلوب")]
        [DisplayName("الايميل")]
        public string Email { get; set; }

        public string? ProfileImage { get; set; }

        public string Mode { get; set; } = "Create";
        
    }
}
