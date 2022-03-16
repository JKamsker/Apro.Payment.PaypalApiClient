
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Text;

namespace Apro.Payment.PaypalApiClient.Models.Web.Payment.Refund
{
    public partial class PaymentRefundRequestDto
    {
        [JsonProperty("amount")]
        public CurrencyDto Amount { get; set; }

        [JsonProperty("invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public string NoteToPayer { get; set; }

        [JsonProperty("invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceId { get; set; }


        public PaymentRefundRequestDto(CurrencyDto amount)
        {
            Amount = amount;
        }
    }
}
