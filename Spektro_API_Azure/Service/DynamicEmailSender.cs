using System;
using System.IO;
using System.Net.Mail;

namespace Spektro_API_Azure.Service
{
    public class DynamicEmailSender
    {
        public static void SendCustomEmail(string firstname, string lastname, string email, DateTime date, string mailtype, string subject)
        {
            string mailContent = "";

            string couponPath = @"EmailAssetFiles\\Coupon.html";
            string forgottenPath = @"EmailAssetFiles\\Forgotten.html";
            string promoPath = @"EmailAssetFiles\\Promo.html";
            string registrationPath = @"EmailAssetFiles\\Registration.html";
            string reservationPath = @"EmailAssetFiles\\Reservation.html";
            string reservationwaiterPath = @"EmailAssetFiles\\ReservationWaiter.html";

            // mailtypes: COUPON, RESERVATION, RESERVATIONWAITER, PROMO, REGISTRATION, FORGOTTEN
            if (mailtype == "COUPON")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + File.ReadAllText(couponPath);
            }
            else if (mailtype == "RESERVATION")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + File.ReadAllText(reservationPath);
            }
            else if (mailtype == "RESERVATIONWAITER")
            {
                mailContent = @"Hello main waiter!<br>" + File.ReadAllText(reservationwaiterPath) + "Name: " + firstname + " " + lastname + "<br>Email: " + email + "<br>Date: " + date;
            }
            else if (mailtype == "PROMO")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + File.ReadAllText(promoPath);
            }
            else if (mailtype == "REGISTRATION")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + File.ReadAllText(registrationPath);
            }
            else if (mailtype == "FORGOTTEN")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + File.ReadAllText(forgottenPath);
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