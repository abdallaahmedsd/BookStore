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
        public const string StatusProgress = "Progress";
        public const string StatusComplete = "Complete";
        public const string StatusCancel = "Cancel";

        // Shipping Status
        public const string StatusOrdered = "Ordered";
        public const string StatusPacked = "Packed";
        public const string StatusInTransit = "InTransit";
        public const string StatusDelivered = "Delivered";


        public static string SessionCart = "SessionShoppingCart";
    }
}
