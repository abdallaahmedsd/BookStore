using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Admin.Order
{
    public class OrderListViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal TotalAmoumt { get; set; } // Typo: Should be "TotalAmount"

        public string Status { get; set; }
    }
}
