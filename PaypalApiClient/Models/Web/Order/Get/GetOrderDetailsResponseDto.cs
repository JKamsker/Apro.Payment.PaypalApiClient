using Apro.Payment.PaypalApiClient.Models.Domain;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;
using System.Collections.Generic;

namespace Apro.Payment.PaypalApiClient.Models.Web.Order.Get
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
        public List<PurchaseUnitDto> PurchaseUnits { get; set; }

        [JsonProperty("create_time")]
        public string CreateTime { get; set; }

        [JsonProperty("links")]
        public List<LinkDto> Links { get; set; }

    }

    public class PurchaseUnitDto
    {
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }

        [JsonProperty("amount")]
        public CurrencyDto Amount { get; set; }

        [JsonProperty("payee")]
        public PayeeDto? Payee { get; set; }

        [JsonProperty("payments")]
        public PaymentsDto? Payments { get; set; }

        public PurchaseUnitDto(string referenceId, CurrencyDto amount, PayeeDto payee = null, PaymentsDto payments = null)
        {
            ReferenceId = referenceId;
            Amount = amount;
            Payee = payee;
            Payments = payments;
        }
    }

    public class PaymentsDto
    {
        [JsonProperty("captures")]
        public List<CaptureDto> Captures { get; set; }
    }

    public class CaptureDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaypalCaptureStatus Status { get; set; }

        [JsonProperty("amount")]
        public CurrencyDto Amount { get; set; }

        [JsonProperty("final_capture")]
        public bool FinalCapture { get; set; }

        //[JsonProperty("seller_protection")]
        //public SellerProtection SellerProtection { get; set; }

        //[JsonProperty("seller_receivable_breakdown")]
        //public SellerReceivableBreakdown SellerReceivableBreakdown { get; set; }

        [JsonProperty("links")]
        public List<LinkDto> Links { get; set; }

        [JsonProperty("create_time")]
        public DateTimeOffset CreateTime { get; set; }

        [JsonProperty("update_time")]
        public DateTimeOffset UpdateTime { get; set; }
    }

    public class PayeeDto
    {
        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }

        [JsonProperty("merchant_id")]
        public string MerchantId { get; set; }
    }
}
