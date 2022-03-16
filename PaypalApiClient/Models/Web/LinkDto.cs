
using Newtonsoft.Json;

using System;

namespace Apro.Payment.PaypalApiClient.Models.Web
{
    public class LinkDto
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("encType")]
        public string EncType { get; set; }
    }
}
