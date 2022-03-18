using Apro.Payment.PaypalApiClient.Models.Domain;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;
using System.Collections.Generic;

namespace Apro.Payment.PaypalApiClient.Models.Web.Order
{
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
}
