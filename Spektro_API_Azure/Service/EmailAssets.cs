using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spektro_API_Azure.Service
{
    public class EmailAssets
    {
        public static string GetCouponHTML() 
        {
            return "<p>Coupon Body</p>";
        }

        public static string GetForgottenHTML()
        {
            return "<p>Forgotten Body</p>";
        }

        public static string GetPromoHTML()
        {
            return "<p>Promo Body</p>";
        }

        public static string GetRegistrationHTML()
        {
           
            return "<p>Registration Body</p>";
        }
        public static string GetReservationHTML()
        {
            return "<p>Reservation Body</p>";
        }
        public static string GetReservationWaiterHTML()
        {
            return "<p>Reservation waiter body</p>";
        }
    }
}
