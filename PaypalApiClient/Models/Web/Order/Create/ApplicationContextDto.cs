
using Newtonsoft.Json;

using System;

namespace Apro.Payment.PaypalApiClient.Models.Web.Order.Create
{
    public class ApplicationContextDto
    {
        [JsonProperty("return_url")]
        public Uri ReturnUrl { get; set; }

        [JsonProperty("cancel_url")]
        public Uri CancelUrl { get; set; }
    }
}
