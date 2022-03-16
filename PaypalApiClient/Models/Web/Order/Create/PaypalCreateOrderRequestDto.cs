
using Apro.Payment.PaypalApiClient.Models.Web.Order.Get;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace Apro.Payment.PaypalApiClient.Models.Web.Order.Create
{
    /// <summary>
    /// The intent to either capture payment immediately or authorize a payment for an order after order creation.
    /// </summary>
    public enum PaypalOrderIntend
    {
        /// <summary>
        /// The merchant intends to capture payment immediately after the customer makes a payment.
        /// </summary>
        [EnumMember(Value = "CAPTURE")]
        Capture,

        /// <summary>
        /// The merchant intends to authorize a payment and place funds on hold after the customer makes a payment. 
        /// Authorized payments are best captured within three days of authorization but are available to capture for up to 29 days. 
        /// After the three-day honor period, the original authorized payment expires and you must re-authorize the payment. 
        /// You must make a separate request to capture payments on demand. 
        /// This intent is not supported when you have more than one `purchase_unit` within your order.
        /// </summary>
        [EnumMember(Value = "AUTHORIZE")]
        Authorize
    }

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

    public class ApplicationContextDto
    {
        [JsonProperty("return_url")]
        public Uri ReturnUrl { get; set; }

        [JsonProperty("cancel_url")]
        public Uri CancelUrl { get; set; }
    }
}
