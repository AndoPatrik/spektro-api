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

            string couponPath = @"EmailAssets\\Coupon.html";
            string forgottenPath = @"EmailAssets\\Forgotten.html";
            string promoPath = @"EmailAssets\\Promo.html";
            string registrationPath = @"EmailAssets\\Registration.html";
            string reservationPath = @"EmailAssets\\Reservation.html";

            // mailtypes: COUPON, RESERVATION, PROMO, REGISTRATION, FORGOTTEN
            if (mailtype == "COUPON")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + File.ReadAllText(couponPath);
            }
            else if (mailtype == "RESERVATION")
            {
                mailContent = @"Hello " + firstname + " " + lastname + ",<br>" + File.ReadAllText(reservationPath);
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
