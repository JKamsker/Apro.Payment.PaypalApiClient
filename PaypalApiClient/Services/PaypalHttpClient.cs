
using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Apro.Payment.PaypalApiClient.Models.Order.Get;
using Apro.Payment.PaypalApiClient.Models.Order.Create;
using Apro.Payment.PaypalApiClient.Models.Token;
using Apro.Payment.PaypalApiClient.Models.Order.Capture;
using Apro.Payment.PaypalApiClient.Http;

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

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PaypalTokenResponseDto>(responseText);
        }

        public async Task<PaypalCreateOrderResponseDto> CreateOrderAsync(string token, PaypalCreateOrderRequestDto createOrderRequestDto)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, "/v2/checkout/orders");
            PrepareHeaders(token, request);

            request.Content = new JsonContent(createOrderRequestDto);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PaypalCreateOrderResponseDto>(responseText);
        }

        public async Task<GetOrderDetailsResponseDto> GetOrderStatusAsync(string token, string orderId)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"/v2/checkout/orders/{orderId}");
            PrepareHeaders(token, request);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetOrderDetailsResponseDto>(responseText);
        }

        public async Task<CaptureOrderResponseDto> CaptureOrderAsync(string token, string orderId)
        {
            ///v2/checkout/orders/{{payment_id}}/capture
            using var request = new HttpRequestMessage(HttpMethod.Post, $"/v2/checkout/orders/{orderId}/capture");
            PrepareHeaders(token, request);

            request.Content = new StringContent("");
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CaptureOrderResponseDto>(responseText);
        }

        private static void PrepareHeaders(string token, HttpRequestMessage request)
        {
            request.Headers.Authorization = new BearerAuthenticationHeaderValue(token);

            PrepareHeaders(request);
        }

        private static void PrepareHeaders(HttpRequestMessage request)
        {
            // https://developer.paypal.com/api/rest/requests/#paypal-request-id
            request.Headers.Add("PayPal-Client-Metadata-Id", "Apro-Smorder");
            request.Headers.Add("PayPal-Request-Id", Guid.NewGuid().ToString());
        }
    }
}
