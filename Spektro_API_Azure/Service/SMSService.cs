using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Spektro_API_Azure.Service
{
    public class SMSService
    {
        public static bool SendSMS(string phoneNo, string text) 
        {
            string accountSid = SecretStrings.GetTwillioSID();
            string authToken = SecretStrings.GetTwillioAuthToken();

            try
            {
                TwilioClient.Init(accountSid, authToken);

                var to = new PhoneNumber(phoneNo);
                var message = MessageResource.Create(
                    to,
                    from: new PhoneNumber("+14242738201"),
                    body: text);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
