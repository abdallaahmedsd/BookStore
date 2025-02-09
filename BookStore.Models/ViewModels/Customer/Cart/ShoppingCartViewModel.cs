using BookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Customer.Cart
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCard> shoppingCartItems { get; set; }

        public Order? Order { get; set; }

    }
}
