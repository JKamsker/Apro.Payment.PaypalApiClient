using Apro.Payment.PaypalApiClient.Models.Domain;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Collections.Generic;

namespace Apro.Payment.PaypalApiClient.Models.Web.Order
{
    public class OrderDto
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
}
