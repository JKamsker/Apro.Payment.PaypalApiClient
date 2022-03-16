using Apro.Payment.PaypalApiClient.Models.Domain;

using System.Threading.Tasks;

namespace Apro.Payment.PaypalApiClient.Services
{
    public class UserScopedPaypalAccessTokenManager
    {
        private readonly PaypalAccessTokenManager _tokenManager;
        private readonly PaypalCredentials _credentials;

        public UserScopedPaypalAccessTokenManager(PaypalAccessTokenManager tokenManager, PaypalCredentials credentials)
        {
            _tokenManager = tokenManager;
            _credentials = credentials;
        }

        public async ValueTask<string> GetAccessTokenAsync(PaypalHttpClient httpClient)
        {
            return await _tokenManager.GetAccessTokenAsync(httpClient, _credentials);
        }
    }
}
