using Apro.Payment.PaypalApiClient.Models.Domain;

using Ardalis.GuardClauses;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Linq;

namespace Apro.Payment.PaypalApiClient.Services
{

    public class PaypalApiClientFactory
    {
        private readonly PaypalAccessTokenManager _accessTokenManager;
        private readonly PaypalHttpClient _httpClient;
        private readonly ApplicationContext _applicationContext;

        public PaypalApiClientFactory
        (
            PaypalAccessTokenManager accessTokenManager, 
            PaypalHttpClient httpClient, 
            ApplicationContext applicationContext = null
        )
        {
            _accessTokenManager = accessTokenManager;
            _httpClient = httpClient;
            _applicationContext = applicationContext;
        }

        public PaypalApiClient Create(PaypalCredentials paypalCredentials, ApplicationContext applicationContext = null)
        {
            var accessTokenManager = new UserScopedPaypalAccessTokenManager(_accessTokenManager, paypalCredentials);
            var appContext = applicationContext
                ?? _applicationContext
                ?? throw new ArgumentNullException(nameof(applicationContext));


            return new PaypalApiClient(_httpClient, accessTokenManager, appContext);
        }

        public PaypalApiClientFactory UseAppContext(ApplicationContext context)
        {
            Guard.Against.Null(context, nameof(context));
            return new PaypalApiClientFactory(_accessTokenManager, _httpClient, context);
        }
    }
}
