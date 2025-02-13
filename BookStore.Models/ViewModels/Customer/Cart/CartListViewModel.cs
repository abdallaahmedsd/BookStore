using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Customer.OrderVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Customer.Cart
{
    public class CartListViewModel
    {
        public List<OrderItemViewModel> CartItems { get; set; }
        public decimal OrderTotalAmount { get; set; }
    }
}
