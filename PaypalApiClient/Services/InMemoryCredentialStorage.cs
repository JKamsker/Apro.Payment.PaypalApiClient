using System.Threading.Tasks;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Apro.Payment.PaypalApiClient.Models.Domain;

namespace Apro.Payment.PaypalApiClient.Services
{
    public class InMemoryCredentialStorage : ICredentialStorage
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private Dictionary<PaypalCredentials, PaypalSessionCredentials> _sessionCredentials = new();

        public InMemoryCredentialStorage(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public async ValueTask<PaypalSessionCredentials> GetCredentialsAsync(PaypalCredentials credentials)
        {
            if (_sessionCredentials.TryGetValue(credentials, out var sessionCredentials))
            {
                if (sessionCredentials.IsExpired(_dateTimeProvider.CurrentDatetimeOffset))
                {
                    _sessionCredentials.Remove(credentials);
                    return null;
                }
                return sessionCredentials;
            }

            return null;
        }

        public async ValueTask SetCredentialsAsync(PaypalCredentials credentials, PaypalSessionCredentials sessionCredentials)
        {
            Guard.Against.Null(sessionCredentials, nameof(sessionCredentials));

            _sessionCredentials[credentials] = sessionCredentials;
        }
    }
}
