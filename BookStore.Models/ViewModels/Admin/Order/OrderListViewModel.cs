using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Customer.OrderVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Admin.Order
{
    public class OrderListViewModel
    {

        List<OrderItemViewModel> OrderItems { get; set; }

    }
}
