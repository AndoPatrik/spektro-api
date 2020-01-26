using System;
using System.IO;
using System.Linq;
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

        private static Random random = new Random();
        public static string GenerateCouponCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string toReturn = "";

            for (int i = 0; i < 4; i++)
            {
                string c = new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
                toReturn = toReturn + c;

                if (i < 3)
                {
                    toReturn = toReturn + "-";
                }
            }
            return toReturn;

            //return new string(Enumerable.Repeat(chars, length)
              //.Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void SendCustomEmail(string firstname, string lastname, string email, DateTime date, string mailtype, string subject, string errorText) //TODO: nullable type
        {
            string mailContentPath = "";
            string mailBodyWithBinding = "";
            StreamReader reader;

            string burger1Name = "TRADITIONAL CHEESY JOE";
            string burger1Description = "This burger was developed by best cooks in Italy in 20th century and according to the original recipe, we are trying to bring the same value to our restaurant! ..";

            string burger2Name = "CLASSIC SUBMARINE";
            string burger2Description = "The straight-forward cheeseburger with special grounded beef that was initially meant for American navy...";

            if (mailtype == "COUPON")
            {
                reader = new StreamReader("EmailTemplates/Spektrocoupon.html");
                mailBodyWithBinding = reader.ReadToEnd()
                    .Replace("{description}", "15% Off from your burger")
                    .Replace("{validfrom}", DateTime.Now.AddDays(7).ToString())
                    .Replace("{validuntil}", date.ToString())
                    .Replace("{code}", GenerateCouponCode());
            }
            else if (mailtype == "RESERVATION")
            {
                reader = new StreamReader("EmailTemplates/Spektrobooking.html");
                mailBodyWithBinding = reader.ReadToEnd()
                    .Replace("{date}", date.Date.ToString())
                    .Replace("{time}", date.TimeOfDay.ToString())
                    .Replace("{burger1 name}", burger1Name)
                    .Replace("{burger1 desription}", burger1Description)
                    .Replace("{burger2 name}", burger2Name)
                    .Replace("{burger2 description}", burger2Description);
            }
            else if (mailtype == "RESERVATIONWAITER")
            {
                mailContentPath = "";
            }
            else if (mailtype == "PROMO")
            {
                mailContentPath = "";
            }
            else if (mailtype == "REGISTRATION")
            {
                reader = new StreamReader("EmailTemplates/Spektrowelcome.html");
                mailBodyWithBinding = reader.ReadToEnd()
                    .Replace("{burger1 name}", burger1Name)
                    .Replace("{burger1 desription}", burger1Description)
                    .Replace("{burger2 name}", burger2Name)
                    .Replace("{burger2 description}", burger2Description)
                    .Replace("{%}", "15%")
                    .Replace("{coupon}","AADE-EFFA-E56S-13AD");
            }
            else if (mailtype == "FORGOTTEN")
            {
                mailContentPath = "";
            }
            else if (mailtype == "ERROR") 
            {
                mailContentPath = GetErrorLogCouldNotBeCreatedHTML(errorText);
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
                msg.Body = mailBodyWithBinding;
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
                    smtpClient.Dispose();
                    //reader.Dispose();
                }
            }

        }
    }
}