
using System;

namespace Apro.Payment.PaypalApiClient.Models.Exceptions
{
    [Serializable]
    public class PaypalLogicException : PayPalException
    {
        public PaypalLogicException()
        {
        }

        public PaypalLogicException(string message) : base(message)
        {
        }
    }
}
