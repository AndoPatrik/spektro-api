using Spektro_API_Azure.Model;
using System;
using System.Net.Mail;

namespace Spektro_API_Azure.Service
{
    public class EmailSenderService
    {
        public static void SendEmailNotificationForReservation(ReservationModel reservation) 
        {
            string addressToSend = reservation.EmailAddress;
            string htmlContent = @"<h1>Dear {firstName}{lastName},</h1>
                                <p>Thank you for your reservation. We will see you on {Date}. To get your table when you arrive please check in at the entrance.
                                <br /> <br /> Have a pleasent time with us! <br /> <br /> Best,<br /> Spektro.sk</p>";


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
                msg = new MailMessage("spektro.noreply@gmail.com", addressToSend);
                msg.Subject = "Spektro.sk - Reservation confirmation";
                msg.Body = htmlContent;
                msg.IsBodyHtml = true;
                smtpClient.Send(msg);
            }
            catch(Exception e)
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
