
using System;

namespace Apro.Payment.PaypalApiClient.Models.Exceptions
{
    [Serializable]
    public class PayPalException : Exception
    {
        public PayPalException()
        {
        }

        public PayPalException(string message) : base(message)
        {
        }
    }
}
