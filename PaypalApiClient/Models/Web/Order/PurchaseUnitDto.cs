
using Newtonsoft.Json;

namespace Apro.Payment.PaypalApiClient.Models.Web.Order
{
    public class PurchaseUnitDto
    {
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }

        [JsonProperty("amount")]
        public CurrencyDto Amount { get; set; }

        [JsonProperty("payee")]
        public PayeeDto Payee { get; set; }

        [JsonProperty("payments")]
        public PaymentsDto Payments { get; set; }

        public PurchaseUnitDto(string referenceId, CurrencyDto amount, PayeeDto payee = null, PaymentsDto payments = null)
        {
            ReferenceId = referenceId;
            Amount = amount;
            Payee = payee;
            Payments = payments;
        }
    }
}
