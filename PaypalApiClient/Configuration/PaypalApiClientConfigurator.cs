using Apro.Payment.PaypalApiClient.Models.Domain;
using Apro.Payment.PaypalApiClient.Models.Exceptions;
using Apro.Payment.PaypalApiClient.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Polly;
using Polly.Contrib.WaitAndRetry;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication.ExtendedProtection;
using System.Text;

namespace Apro.Payment.PaypalApiClient.Configuration
{
    public class PaypalApiClientConfigurator
    {
        private readonly IServiceCollection _serviceCollection;

        public static PaypalApiClientConfigurator New => new(new ServiceCollection());
        public PaypalApiClientConfigurator(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public string ApiServer { get; private set; }

        public string ClientName { get; private set; }
        public ApplicationContext AppContext { get; private set; }

        public PaypalApiClientConfigurator UseProduction()
            => UseServer("https://api-m.paypal.com");

        public PaypalApiClientConfigurator UseDevelopment()
            => UseServer("https://api-m.sandbox.paypal.com");

        public PaypalApiClientConfigurator UseServer(string serverAdress)
        {
            ApiServer = serverAdress;
            return this;
        }

        public PaypalApiClientConfigurator WithClientName(string clientName)
        {
            ClientName = clientName;
            return this;
        }

        public PaypalApiClientConfigurator WithReturnUrls(string successUrl, string cancelUrl)
        {
            AppContext = new ApplicationContext(successUrl, cancelUrl);
            return this;
        }

        public void Apply()
        {
            TryRegisterHttpClient();

            _serviceCollection.TryAddTransient<PaypalApiClientFactory>();

            _serviceCollection.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();
            _serviceCollection.TryAddSingleton<ICredentialStorage, InMemoryCredentialStorage>();
            _serviceCollection.TryAddSingleton<PaypalAccessTokenManager>();

            if (AppContext is not null)
            {
                _serviceCollection.TryAddSingleton(AppContext);
            }
        }

        private void TryRegisterHttpClient()
        {
            if (_serviceCollection.Any(x => x.ServiceType == typeof(PaypalHttpClient)))
            {
                return;
            }

            if (string.IsNullOrEmpty(ApiServer))
            {
                throw new PayPalException
                (
                    $"No api server specified. " +
                    $"Please use {nameof(UseProduction)} or {nameof(UseDevelopment)} to specify the environment"
                );
            }


            _serviceCollection.AddHttpClient<PaypalHttpClient>(x =>
            {
                x.BaseAddress = new Uri(ApiServer);

                x.DefaultRequestHeaders.Add("PayPal-Client-Metadata-Id", ClientName);
                x.DefaultRequestHeaders.Add("Prefer", "return=representation");
            })
            .AddPolicyHandler(msg =>
            {
                var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 10);

                return Policy<HttpResponseMessage>
                    .Handle<HttpRequestException>()
                    .OrResult(x => (int)x.StatusCode == 429 /*HttpStatusCode.TooManyRequests*/ )
                    .OrResult(x => (int)x.StatusCode >= (int)HttpStatusCode.InternalServerError && (int)x.StatusCode <= 599)
                    .WaitAndRetryAsync(delay);
            });
        }

        public IServiceProvider BuildServiceProvider()
        {
            Apply();
            return _serviceCollection.BuildServiceProvider();
        }

        public PaypalApiClientFactoryFactory Build() 
            => new(BuildServiceProvider());
    }
}
