using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Spektro_API_Azure.Service
{
    public class SMSSender
    {
        public static bool SendSMS(string phoneNo) 
        {
            string accountSid = ConnectionString.GetTwillioSID();
            string authToken = ConnectionString.GetTwillioAuthToken();

            try
            {
                TwilioClient.Init(accountSid, authToken);

                var to = new PhoneNumber(phoneNo);
                var message = MessageResource.Create(
                    to,
                    from: new PhoneNumber("+14242738201"),
                    body: "Hello Spektro Customer, welcome from Spektro API");
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }
    }
}
