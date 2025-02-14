using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Customer.OrderVM
{
    public class OrderListForCustomerViewModel
    {
        public int Id { get; set; }

        [DisplayName("تاريخ الطلب")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("المحافظة")]
        public string Status { get; set; }

        [DisplayName("العنوان")]
        public decimal TotalAmoumt { get; set; } // Typo: Should be "TotalAmount"

        [DisplayName("رقم التتبع")]
        public string TrackingNumber { get; set; }

        [DisplayName("شركة النقل")]
        public string? Carrier { get; set; }
    }
}
