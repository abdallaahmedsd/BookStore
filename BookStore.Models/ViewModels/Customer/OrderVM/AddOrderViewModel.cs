using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Customer.OrderVM
{
    // that I will send from the summar when pay now clicked
    public class AddOrderViewModel
    {
        public int Id { get; set; }

        [DisplayName("البلد")]
        public string Country { get; set; }

        [DisplayName("المحافظة")]
        public string State { get; set; }

        [DisplayName("العنوان")]
        public string ShippingAddress { get; set; }

        [DisplayName("الرقم البريدي")]
        public string TrackingNumber { get; set; }
    }
}
