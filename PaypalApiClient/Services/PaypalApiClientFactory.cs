using PaypalPaymentProvider.Models;
using PaypalPaymentProvider.Models.Order.Create;

namespace PaypalPaymentProvider.Services
{
    public class PaypalApiClientFactory
    {
        private readonly PaypalAccessTokenManager _accessTokenManager;
        private readonly PaypalHttpClient _httpClient;
        private readonly ApplicationContext _applicationContext;

        public PaypalApiClientFactory(PaypalAccessTokenManager accessTokenManager, PaypalHttpClient httpClient, ApplicationContext applicationContext)
        {
            _accessTokenManager = accessTokenManager;
            _httpClient = httpClient;
            _applicationContext = applicationContext;
        }

        public PaypalApiClient Create(PaypalCredentials paypalCredentials, ApplicationContext applicationContext = null)
        {
            var accessTokenManager = new UserScopedPaypalAccessTokenManager(_accessTokenManager, paypalCredentials);
            return new PaypalApiClient(_httpClient, accessTokenManager, applicationContext ?? _applicationContext);
        }
    }
}
