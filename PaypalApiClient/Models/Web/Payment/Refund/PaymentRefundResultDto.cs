
using Apro.Payment.PaypalApiClient.Models.Web.Order;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Apro.Payment.PaypalApiClient.Models.Web.Payment.Refund
{
    public class PaymentRefundResultDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("amount")]
        public CurrencyDto Amount { get; set; }

        [JsonProperty("note_to_payer")]
        public string NoteToPayer { get; set; }

        //[JsonProperty("seller_payable_breakdown")]
        //public SellerPayableBreakdown SellerPayableBreakdown { get; set; }

        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }


        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaypalRefundStatus Status { get; set; }

        [JsonProperty("create_time")]
        public DateTimeOffset CreateTime { get; set; }

        [JsonProperty("update_time")]
        public DateTimeOffset UpdateTime { get; set; }

        [JsonProperty("links")]
        public List<LinkDto> Links { get; set; }
    }

    public enum PaypalRefundStatus
    {
        Default,

        /// <summary>
        /// The refund was cancelled.
        /// </summary>
        [EnumMember(Value = "CANCELLED")]
        Cancelled,

        /// <summary>
        /// The refund is pending. 
        /// For more information, see status_details.reason.
        /// </summary>
        [EnumMember(Value = "PENDING")]
        Pending,

        /// <summary>
        /// The funds for this transaction were debited to the customer's account.
        /// </summary>
        [EnumMember(Value = "COMPLETED")]
        Completed
    }


    //public partial class SellerPayableBreakdown
    //{
    //    [JsonProperty("gross_amount")]
    //    public CurrencyDto GrossAmount { get; set; }

    //    [JsonProperty("paypal_fee")]
    //    public CurrencyDto PaypalFee { get; set; }

    //    [JsonProperty("net_amount")]
    //    public CurrencyDto NetAmount { get; set; }

    //    [JsonProperty("total_refunded_amount")]
    //    public CurrencyDto TotalRefundedAmount { get; set; }
    //}
}
