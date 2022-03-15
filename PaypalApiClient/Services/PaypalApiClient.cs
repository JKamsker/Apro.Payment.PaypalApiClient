using System.Threading.Tasks;
using System.Linq;
using Ardalis.GuardClauses;
using Apro.Payment.PaypalApiClient.Models;
using Apro.Payment.PaypalApiClient.Extensions;
using Apro.Payment.PaypalApiClient.Models.Order;
using Apro.Payment.PaypalApiClient.Models.Order.Create;

namespace Apro.Payment.PaypalApiClient.Services
{
    public class PaypalApiClient
    {
        private readonly PaypalHttpClient _httpClient;
        private readonly UserScopedPaypalAccessTokenManager _tokenManager;
        private readonly ApplicationContext _applicationContext;

        public PaypalApiClient(PaypalHttpClient httpClient, UserScopedPaypalAccessTokenManager tokenManager, ApplicationContext applicationContext)
        {
            Guard.Against.Null(httpClient, nameof(httpClient));
            Guard.Against.Null(tokenManager, nameof(tokenManager));
            Guard.Against.Null(applicationContext, nameof(applicationContext));

            _httpClient = httpClient;
            _tokenManager = tokenManager;
            _applicationContext = applicationContext;
        }

        public async Task<PaypalOrder> CreateOrderAsync(params PurchaseUnit[] purchaseUnits)
        {
            Guard.Against.Empty(purchaseUnits, nameof(purchaseUnits));

            var token = await _tokenManager.GetAccessTokenAsync(_httpClient);
            var orderResponse = await _httpClient.CreateOrderAsync(token, new PaypalCreateOrderRequestDto()
            {
                Intent = PaypalOrderIntend.Capture,
                ApplicationContext = _applicationContext.AsDto(),
                PurchaseUnits = purchaseUnits.Select(x => x.AsDto()).ToList()
            });

            return PaypalOrder.FromDto(orderResponse);
        }

        public async Task<PaypalOrder> GetOrderAsync(string orderId)
        {
            Guard.Against.NullOrEmpty(orderId, nameof(orderId));
            var token = await _tokenManager.GetAccessTokenAsync(_httpClient);
            var orderResponse = await _httpClient.GetOrderStatusAsync(token, orderId);

            return PaypalOrder.FromDto(orderResponse);
        }

        public async Task<PaypalOrder> CaptureOrderAsync(string orderId)
        {
            Guard.Against.NullOrEmpty(orderId, nameof(orderId));
            var token = await _tokenManager.GetAccessTokenAsync(_httpClient);
            var orderResponse = await _httpClient.CaptureOrderAsync(token, orderId);

            return PaypalOrder.FromDto(orderResponse);
        }
    }
}
