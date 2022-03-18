
using Newtonsoft.Json;

using System.Collections.Generic;

namespace Apro.Payment.PaypalApiClient.Models.Web.Order
{
    public class PaymentsDto
    {
        [JsonProperty("captures")]
        public List<CaptureDto> Captures { get; set; }
    }
}
