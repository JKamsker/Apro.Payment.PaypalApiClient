using System.Threading.Tasks;
using System.Linq;
using Ardalis.GuardClauses;
using Apro.Payment.PaypalApiClient.Extensions;
using Apro.Payment.PaypalApiClient.Models.Web.Order.Create;
using Apro.Payment.PaypalApiClient.Models.Domain;
using Apro.Payment.PaypalApiClient.Models;
using Apro.Payment.PaypalApiClient.Models.Web.Payment.Refund;
using System.Collections.Generic;
using Apro.Payment.PaypalApiClient.Models.Exceptions;

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
                ApplicationContext = DtoMapper.MapApplicationContext(_applicationContext),
                PurchaseUnits = purchaseUnits.Select(x => DtoMapper.MapPurchaseUnit(x)).ToList()
            });

            return DomainMapper.MapOrder(orderResponse);
        }

        public async Task<PaypalOrder> GetOrderAsync(string orderId)
        {
            Guard.Against.NullOrEmpty(orderId, nameof(orderId));
            var token = await _tokenManager.GetAccessTokenAsync(_httpClient);
            var orderResponse = await _httpClient.GetOrderDetailsAsync(token, orderId);
            return DomainMapper.MapOrder(orderResponse);
        }

        public async Task<PaypalOrder> CaptureOrderAsync(string orderId)
        {
            Guard.Against.NullOrEmpty(orderId, nameof(orderId));
            var token = await _tokenManager.GetAccessTokenAsync(_httpClient);
            var orderResponse = await _httpClient.CaptureOrderAsync(token, orderId);

            return DomainMapper.MapOrder(orderResponse);
        }

        public async Task<List<PaypalRefund>> RefundOrderAsync(string orderId, string reason = null)
        {
            var results = new List<PaypalRefund>();
            var order = await GetOrderAsync(orderId);
            if (order.Status != PaypalOrderStatus.Completed)
            {
                throw new PaypalLogicException
                (
                    $"Order has an invalid status: {order.Status}. " +
                    $"Expected {PaypalOrderStatus.Completed}"
                );
            }

            foreach (var purchaseUnit in order.PurchaseUnits)
            {
                foreach (var capture in purchaseUnit.Captures)
                {
                    if (capture.Status == PaypalCaptureStatus.Completed)
                    {
                        continue;
                    }

                    if (capture.Status != PaypalCaptureStatus.Completed)
                    {
                        continue;
                    }

                    var token = await _tokenManager.GetAccessTokenAsync(_httpClient);

                    var currency = DtoMapper.MapAmount(capture.Amount);
                    var refundResult = await _httpClient.RefundPaymentAsync(token, capture.Id, new PaymentRefundRequestDto(currency)
                    {
                        NoteToPayer = reason
                    });

                    results.Add(new PaypalRefund
                    {
                        Id = refundResult.Id,
                        Amount = DomainMapper.MapAmount(refundResult.Amount),
                        Reason = refundResult.NoteToPayer,
                        Status = refundResult.Status
                    });
                }
            }

            return results;
        }
    }
}
