using BookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Customer.OrderVM
{
    // Information to show at summary
    public class OrderSummaryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "البلد مطلوب")]
        [DisplayName("البلد")]
        public int CountryId { get; set; } 

        [Required(ErrorMessage = "المحافظة مطلوبة")]
        [DisplayName("المحافظة")]
        public string City { get; set; }

        [Required(ErrorMessage = "العنوان مطلوب")]
        [DisplayName("العنوان")]
        public string Address { get; set; }

        [Required(ErrorMessage = "الرقم البريدي مطلوب")]
        [DisplayName("الرقم البريدي")]
        public string ZipCode { get; set; }
        
        public List<OrderItemViewModel> CartItems { get; set; } = [];
        public List<Country> Countries { get; set; } = [];
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public decimal OrderTotalAmount { get; set; } = 0;
        public DateTime EstimatedDelivery { get; set; }

    }
}
