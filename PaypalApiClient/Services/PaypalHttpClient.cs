
using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Apro.Payment.PaypalApiClient.Http;
using Apro.Payment.PaypalApiClient.Models.Web.Order.Get;
using Apro.Payment.PaypalApiClient.Models.Web.Order.Create;
using Apro.Payment.PaypalApiClient.Models.Web.Token;
using Apro.Payment.PaypalApiClient.Models.Web.Payment.Refund;
using Apro.Payment.PaypalApiClient.Models.Web.Error;
using Apro.Payment.PaypalApiClient.Models.Exceptions;

namespace Apro.Payment.PaypalApiClient.Services
{
    public class PaypalHttpClient
    {
        private readonly HttpClient _httpClient;

        public PaypalHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaypalTokenResponseDto> GetAccessToken(string userName, string secret)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, "/v1/oauth2/token");
            request.Headers.Authorization = new BasicAuthenticationHeaderValue(userName, secret);
            PrepareHeaders(request);

            request.Content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("grant_type", "client_credentials") });

            return await ExecuteRequestAsync<PaypalTokenResponseDto>(request);
        }

        private async Task<T> ExecuteRequestAsync<T>(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            await HandleUnsuccessfulResponse(response);

            var responseText = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseText);
        }

        public async Task<GetOrderDetailsResponseDto> CreateOrderAsync(string token, PaypalCreateOrderRequestDto createOrderRequestDto)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, "/v2/checkout/orders");
            PrepareHeaders(token, request);
            request.Content = new JsonContent(createOrderRequestDto);

            return await ExecuteRequestAsync<GetOrderDetailsResponseDto>(request);
        }

        public async Task<GetOrderDetailsResponseDto> GetOrderDetailsAsync(string token, string orderId)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"/v2/checkout/orders/{orderId}");
            PrepareHeaders(token, request);
            return await ExecuteRequestAsync<GetOrderDetailsResponseDto>(request);
        }

        public async Task<GetOrderDetailsResponseDto> CaptureOrderAsync(string token, string orderId)
        {
            ///v2/checkout/orders/{{payment_id}}/capture
            using var request = new HttpRequestMessage(HttpMethod.Post, $"/v2/checkout/orders/{orderId}/capture");
            PrepareHeaders(token, request);

            request.Content = new StringContent("");
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            return await ExecuteRequestAsync<GetOrderDetailsResponseDto>(request);
        }

        public async Task<PaymentRefundResultDto> RefundPaymentAsync(string token, string paymentCaptureId, PaymentRefundRequestDto refundRequest)
        {
            ///v2/payments/captures/5JJ41332CP233092P/refund
            using var request = new HttpRequestMessage(HttpMethod.Post, $"/v2/payments/captures/{paymentCaptureId}/refund");
            PrepareHeaders(token, request);
            request.Content = new JsonContent(refundRequest);
            return await ExecuteRequestAsync<PaymentRefundResultDto>(request);
        }

        private static async Task HandleUnsuccessfulResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            PaypalErrorResultDto result;
            string responseText;
            try
            {
                responseText = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<PaypalErrorResultDto>(responseText);
            }
            catch
            {
                response.EnsureSuccessStatusCode();
                return;
            }

            throw new PaypalRequestException(result);
        }

        private static void PrepareHeaders(string token, HttpRequestMessage request)
        {
            request.Headers.Authorization = new BearerAuthenticationHeaderValue(token);

            PrepareHeaders(request);
        }

        private static void PrepareHeaders(HttpRequestMessage request)
        {
            // https://developer.paypal.com/api/rest/requests/#paypal-request-id
            request.Headers.Add("PayPal-Request-Id", Guid.NewGuid().ToString());
        }
    }
}
