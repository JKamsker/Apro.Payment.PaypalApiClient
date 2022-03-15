
using System;

namespace Apro.Payment.PaypalApiClient.Services
{
    public class PaypalSessionCredentials
    {
        public string AccessToken { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }

        public PaypalSessionCredentials()
        {

        }

        public PaypalSessionCredentials(string bearerToken, DateTimeOffset expiresAt)
        {
            AccessToken = bearerToken;
            ExpiresAt = expiresAt;
        }

        public bool IsExpired(DateTimeOffset? now = null)
        {
            return (now ?? DateTimeOffset.UtcNow) > ExpiresAt - TimeSpan.FromMinutes(5);
        }
    }
}
