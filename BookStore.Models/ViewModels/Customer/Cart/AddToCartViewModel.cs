using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Customer.Cart
{
    public class AddToCartViewModel
    {
        public int BookId { get; set; }

        [Range(1, 1000)]
        public int Quantity { get; set; }
    }
}
