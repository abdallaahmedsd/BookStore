using BookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Customer.OrderVM
{
    // Information to show at summary
    public class OrderSummaryViewModel
    {
        public List<OrderItemViewModel> CartItems { get; set; }
        public decimal OrderTotalAmount { get; set; } 
        public DateTime EstimatedDelivery { get; set; }
        public int UserID { get; set;}
    }
}
