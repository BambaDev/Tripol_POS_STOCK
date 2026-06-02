using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Function
{
    internal class PaymentService
    {
        private string apiKey = "your_stripe_api_key";

        public PaymentService()
        {
            StripeConfiguration.ApiKey = apiKey;
        }

        public Charge CreateCharge(decimal amount, string currency, string sourceToken)
        {
            var chargeOptions = new ChargeCreateOptions
            {
                Amount = (long)(amount * 100), // convert amount to cents
                Currency = currency,
                Source = sourceToken,
                Description = "Example charge"
            };

            var chargeService = new ChargeService();
            Charge charge = chargeService.Create(chargeOptions);
            return charge;
        }
    }
}
