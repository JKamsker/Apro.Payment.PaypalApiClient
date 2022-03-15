
using Apro.Payment.PaypalApiClient.Models;

using System;
using System.Threading.Tasks;

namespace Apro.Payment.PaypalApiClient.Services
{
    public class PaypalAccessTokenManager
    {
        private readonly ICredentialStorage _credentialStorage;
        private readonly IDateTimeProvider _dateTimeProvider;

        public PaypalAccessTokenManager(ICredentialStorage credentialStorage, IDateTimeProvider dateTimeProvider)
        {
            _credentialStorage = credentialStorage;
            _dateTimeProvider = dateTimeProvider;
        }

        public async ValueTask<string> GetAccessTokenAsync(PaypalHttpClient httpClient, PaypalCredentials credentials)
        {
            var creds = await _credentialStorage.GetCredentialsAsync(credentials);
            if (creds is null || creds.IsExpired(_dateTimeProvider.CurrentDatetimeOffset))
            {
                var token = await httpClient.GetAccessToken(credentials.UserName, credentials.Secret);
                await _credentialStorage.SetCredentialsAsync(credentials, new(token.AccessToken, DateTimeOffset.UtcNow + TimeSpan.FromSeconds(token.ExpiresIn)));
                return token.AccessToken;
            }

            return creds.AccessToken;
        }
    }
}
