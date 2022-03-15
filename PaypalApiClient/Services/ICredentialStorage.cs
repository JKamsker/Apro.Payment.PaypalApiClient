
using PaypalPaymentProvider.Models;

using System.Threading.Tasks;

namespace PaypalPaymentProvider.Services
{
    public interface ICredentialStorage
    {
        ValueTask<PaypalSessionCredentials> GetCredentialsAsync(PaypalCredentials credentials);
        ValueTask SetCredentialsAsync(PaypalCredentials credentials, PaypalSessionCredentials sessionCredentials);
    }
}
