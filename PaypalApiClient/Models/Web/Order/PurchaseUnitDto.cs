
using Newtonsoft.Json;

namespace Apro.Payment.PaypalApiClient.Models.Web.Order
{
    public class PurchaseUnitDto
    {
        [JsonProperty("reference_id", NullValueHandling = NullValueHandling.Ignore)]

        public string ReferenceId { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("amount")]
        public CurrencyDto Amount { get; set; }

        [JsonProperty("payee")]
        public PayeeDto Payee { get; set; }

        [JsonProperty("payments")]
        public PaymentsDto Payments { get; set; }

        public PurchaseUnitDto(CurrencyDto amount)
        {
            Amount = amount;
        }
    }
}
