using BookStore.Models.ViewModels.Customer.OrderVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Admin.Order
{
    public class ManageOrderViewModel
    {
        // Order items:
        public List<OrderItemViewModel> OrderItems { get; set; } = [];

        //Order Info:
        public int Id { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalAmoumt { get; set; } // Typo: Should be "TotalAmount"
        public string Status { get; set; }

        // Shipping Info:
        [DisplayName("البلد")]
        public string CountryName { get; set; }
        [DisplayName("المحافظة")]
        public string City { get; set; }
        [DisplayName("العنوان")]
        public string Address { get; set; }
        [DisplayName("الرقم البريدي")]
        public string ZipCode { get; set; }

        [DisplayName("شركة النقل")]
        public string? Carrier { get; set; }

        [DisplayName("رقم التتبع")]
        public string? TrackingNumber { get; set; }

        [DisplayName("تاريخ الشحن")]
        public DateTime? ShippingDate { get; set; } = DateTime.Now;
        public DateTime EstimatedDelivery { get; set; }  // Needed : it could still the same (10 days difference) or the admin could change it

        // User Info:
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }
}
