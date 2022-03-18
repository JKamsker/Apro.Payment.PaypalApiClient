
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Collections.Generic;
using System.Globalization;

namespace Apro.Payment.PaypalApiClient.Models.Web.Order.Create
{

    public class PaypalCreateOrderRequestDto
    {
        /// <summary>
        /// The intent to either capture payment immediately or authorize a payment for an order after order creation.
        /// </summary>
        [JsonProperty("intent")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaypalOrderIntend Intent { get; set; }

        [JsonProperty("application_context")]
        public ApplicationContextDto ApplicationContext { get; set; }

        [JsonProperty("purchase_units")]
        public List<PurchaseUnitDto> PurchaseUnits { get; set; }
    }
}
