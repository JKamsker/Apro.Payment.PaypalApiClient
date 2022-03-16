
using Newtonsoft.Json;

using System;

namespace Apro.Payment.PaypalApiClient.Models.Domain
{
    public class Link
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }
    }
}
