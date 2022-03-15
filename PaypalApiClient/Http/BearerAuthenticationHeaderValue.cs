
using System.Net.Http.Headers;

namespace PaypalPaymentProvider.Http
{
    public class BearerAuthenticationHeaderValue : AuthenticationHeaderValue
    {
        public BearerAuthenticationHeaderValue(string parameter) : base("Bearer", parameter)
        {
        }
    }
}
