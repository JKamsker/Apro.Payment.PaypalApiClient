
using System.Net.Http.Headers;

namespace Apro.Payment.PaypalApiClient.Http
{
    public class BearerAuthenticationHeaderValue : AuthenticationHeaderValue
    {
        public BearerAuthenticationHeaderValue(string parameter) : base("Bearer", parameter)
        {
        }
    }
}
