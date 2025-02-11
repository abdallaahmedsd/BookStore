using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utilties
{
    public static class SessionHelper
    {
        // Order Status
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCanceled = "Canceled";

        // in Session Storage
        public static string SessionCart = "SessionShoppingCart";
        public static string SessionOrderStatus = "SessionOrderStatus";
    }
}
