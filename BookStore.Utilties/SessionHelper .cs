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
        public const string StatusApproved = "تم الموافقة";
        public const string StatusInProcess = "قيد الانتظار";
        public const string StatusShipped = "تم شحنه";
        public const string StatusCanceled = "تم إلغائه";

        // Estimated Delivery
        public const int EstimatedDeliveryPeriod = 10;


        // in Session Storage
        public static string SessionCart = "SessionShoppingCart";
        public static string SessionOrderStatus = "SessionOrderStatus";
    }
}
