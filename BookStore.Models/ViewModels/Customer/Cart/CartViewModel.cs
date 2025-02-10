using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Customer.Cart
{
    public class CartViewModel
    {
        public int Id { get; set; }

        public int BookID { get; set; }
        public string BookCoverImage { get; set; }
        public string BookTitle { get; set; }
        public decimal BookPrice { get; set; }
        public decimal SubTotal { get; set; }
        public int Quantity { get; set; }

        public int UserID { get; set; }
    }
}
