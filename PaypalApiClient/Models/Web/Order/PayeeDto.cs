
using Newtonsoft.Json;

namespace Apro.Payment.PaypalApiClient.Models.Web.Order
{
    public class PayeeDto
    {
        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }

        [JsonProperty("merchant_id")]
        public string MerchantId { get; set; }
    }
}
