using Apro.Payment.PaypalApiClient.Models.Domain;

using System.Threading.Tasks;

namespace Apro.Payment.PaypalApiClient.Services
{
    public interface ICredentialStorage
    {
        ValueTask<PaypalSessionCredentials> GetCredentialsAsync(PaypalCredentials credentials);
        ValueTask SetCredentialsAsync(PaypalCredentials credentials, PaypalSessionCredentials sessionCredentials);
    }
}
