
using System;
using System.Net.Http.Headers;
using System.Text;

namespace Apro.Payment.PaypalApiClient.Http
{
    public class BasicAuthenticationHeaderValue : AuthenticationHeaderValue
    {
        public BasicAuthenticationHeaderValue(string userName, string secret) : base("Basic", GetBasicValue(userName, secret))
        {
        }

        private static string GetBasicValue(string userName, string secret)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{secret}"));
        }
    }
}
