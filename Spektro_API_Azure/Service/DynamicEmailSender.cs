using System;
using System.Net.Mail;

namespace Spektro_API_Azure.Service
{
    public class DynamicEmailSender
    {
        public static void SendCustomEmail(string firstname, string lastname, string email, DateTime date, string mailtype, string subject)
        {
            string mailContent = "";

          
            // mailtypes: COUPON, RESERVATION, RESERVATIONWAITER, PROMO, REGISTRATION, FORGOTTEN
            if (mailtype == "COUPON")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + EmailAssets.GetCouponHTML();
            }
            else if (mailtype == "RESERVATION")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + EmailAssets.GetReservationHTML();
            }
            else if (mailtype == "RESERVATIONWAITER")
            {
                mailContent = @"Hello main waiter!<br>" + EmailAssets.GetReservationWaiterHTML() + "Name: " + firstname + " " + lastname + "<br>Email: " + email + "<br>Date: " + date;
            }
            else if (mailtype == "PROMO")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + EmailAssets.GetPromoHTML();
            }
            else if (mailtype == "REGISTRATION")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + EmailAssets.GetReservationHTML();
            }
            else if (mailtype == "FORGOTTEN")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + EmailAssets.GetForgottenHTML();
            };


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