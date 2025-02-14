using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utilties
{
    public static class Global
    {
        public static string SetOrderStatus(byte Status)
        {
            switch (Status)
            {
                case 1:
                    return SessionHelper.StatusApproved;
                case 2:
                    return SessionHelper.StatusInProcess;
                case 3:
                    return SessionHelper.StatusShipped;
                case 4:
                    return SessionHelper.StatusCanceled;
                default:
                    return "غير معروف";
            }

        }

        public static byte SetOrderStatus(string Status)
        {
            switch (Status)
            {
                case SessionHelper.StatusApproved:
                    return 1;
                case SessionHelper.StatusInProcess:
                    return 2;
                case SessionHelper.StatusShipped:
                    return 3;
                case SessionHelper.StatusCanceled:
                    return 4;
                default:
                    return 0;
            }

        }
    }
}
