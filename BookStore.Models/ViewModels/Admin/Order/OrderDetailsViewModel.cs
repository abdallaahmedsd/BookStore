using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Customer.OrderVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Admin.Order
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public int UserID { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal TotalAmoumt { get; set; } // Typo: Should be "TotalAmount"

        public byte Status { get; set; }

        List<OrderItemViewModel> OrderItems { get; set; }

        public byte OrderStatus { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string? TrackingNumber { get; set; }
        public DateTime? EstimatedDelivery { get; set; }
        public byte ShippingStatus { get; set; }
    }
}
