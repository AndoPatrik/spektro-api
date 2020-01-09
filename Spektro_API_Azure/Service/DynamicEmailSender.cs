using System;
using System.Net.Mail;

namespace Spektro_API_Azure.Service
{
    public class DynamicEmailSender
    {
        #region EmailContents
        private static string GetCouponHTML()
        {
            return "<p>Coupon Body</p>";
        }

        private static string GetForgottenHTML()
        {
            return "<p>Forgotten Body</p>";
        }

        private static string GetPromoHTML()
        {
            return "<p>Promo Body</p>";
        }

        private static string GetRegistrationHTML()
        {

            return "<p>Registration Body</p>";
        }
        private static string GetReservationHTML()
        {
            return "<p>Reservation Body</p>";
        }
        private static string GetReservationWaiterHTML()
        {
            return "<p>Reservation waiter body</p>";
        }
        private static string GetErrorLogCouldNotBeCreatedHTML(string errorText) 
        {
            string log = "There was no log info";
            if (errorText != String.Empty || errorText != null) 
            {
                log = errorText;
            }
            return $"<p>Error log could not be created: {log}</p>";
        }
        #endregion

        public static void SendCustomEmail(string firstname, string lastname, string email, DateTime date, string mailtype, string subject, string errorText) //TODO: nullable type
        {
            string mailContent = "";

            if (mailtype == "COUPON")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + GetCouponHTML();
            }
            else if (mailtype == "RESERVATION")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + GetReservationHTML();
            }
            else if (mailtype == "RESERVATIONWAITER")
            {
                mailContent = @"Hello main waiter!<br>" + GetReservationWaiterHTML() + "Name: " + firstname + " " + lastname + "<br>Email: " + email + "<br>Date: " + date;
            }
            else if (mailtype == "PROMO")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + GetPromoHTML();
            }
            else if (mailtype == "REGISTRATION")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + GetReservationHTML();
            }
            else if (mailtype == "FORGOTTEN")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + GetForgottenHTML();
            }
            else if (mailtype == "ERROR") 
            {
                mailContent = GetErrorLogCouldNotBeCreatedHTML(errorText);
            }


            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            MailMessage msg = null;

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.EnableSsl = true;

            string emailSenderUserName = "spektro.noreply@gmail.com";
            string emailSenderPw = "Spektro2019";
            smtpClient.Credentials = new System.Net.NetworkCredential(emailSenderUserName, emailSenderPw);

            try
            {
                msg = new MailMessage("spektro.noreply@gmail.com", email);
                msg.Subject = subject;
                msg.Body = mailContent;
                msg.IsBodyHtml = true;
                smtpClient.Send(msg);
            }
            catch (Exception e)
            {
                throw e;
                //TODO: Log exception to db
            }
            finally
            {
                if (msg != null)
                {
                    msg.Dispose();
                }
            }

        }
    }
}