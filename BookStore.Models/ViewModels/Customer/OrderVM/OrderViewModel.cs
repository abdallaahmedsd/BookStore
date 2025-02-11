using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Customer.OrderVM
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public decimal TotalAmoumt { get; set; } // Typo: Should be "TotalAmount"
        public DateTime ShippingDate { get; set; }
        public DateTime EstimatedDelivery { get; set; }
        public byte Status { get; set; }
        public int UserID { get; set; }
    }
}
