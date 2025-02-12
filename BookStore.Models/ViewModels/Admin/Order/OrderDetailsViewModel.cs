using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Customer.OrderVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels.Admin.Order
{
    public class OrderDetailsViewModel
    {

        //Order Info:
        public int Id { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalAmoumt { get; set; } // Typo: Should be "TotalAmount"
        public byte Status { get; set; }

        // Shipping Info:
        public string CountryName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public DateTime EstimatedDelivery { get; set; }

        // User Info:
        public string FullName { get; set; }
        public string Email { get; set; }

    }
}
