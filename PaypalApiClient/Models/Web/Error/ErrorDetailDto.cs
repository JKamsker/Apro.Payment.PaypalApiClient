using Newtonsoft.Json;

namespace Apro.Payment.PaypalApiClient.Models.Web.Error
{
    public class ErrorDetailDto
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("issue")]
        public string Issue { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
