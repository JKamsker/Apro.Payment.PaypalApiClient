using Apro.Payment.PaypalApiClient.Services;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace Apro.Payment.PaypalApiClient.Configuration
{
    public class PaypalApiClientFactoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PaypalApiClientFactoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public PaypalApiClientFactory Create() => _serviceProvider.GetRequiredService<PaypalApiClientFactory>();
    }
}
