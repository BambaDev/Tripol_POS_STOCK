using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Pos.Function
{
    internal class SmsSender
    {
        private readonly string _accountSid;
        private readonly string _authToken;

        public SmsSender(string accountSid, string authToken)
        {
            _accountSid = accountSid;
            _authToken = authToken;
        }

        public void SendSmsMessageAsync(string from,string to, string message)
        {
            TwilioClient.Init(_accountSid, _authToken);

            var messageOptions = new CreateMessageOptions(
              new PhoneNumber("whatsapp:+213" + to)
            );

            messageOptions.From = new PhoneNumber("whatsapp:+141" + from);
            messageOptions.Body = message;

            var messageDetails = MessageResource.Create(messageOptions);
        }
    }
}
