
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Collections.Generic;

namespace Apro.Payment.PaypalApiClient.Models.Order.Get
{
    public class GetOrderDetailsResponseDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("intent")]
        public string Intent { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaypalOrderStatus Status { get; set; }

        [JsonProperty("purchase_units")]
        public List<PurchaseUnit> PurchaseUnits { get; set; }

        [JsonProperty("create_time")]
        public string CreateTime { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }

    }

    public class PurchaseUnitDto
    {
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }

        [JsonProperty("amount")]
        public AmountDto Amount { get; set; }

        [JsonProperty("payee")]
        public PayeeDto Payee { get; set; }
    }

    public class AmountDto
    {
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class PayeeDto
    {
        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }

        [JsonProperty("merchant_id")]
        public string MerchantId { get; set; }
    }
}
