using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Text;

namespace Apro.Payment.PaypalApiClient.Models.Web.Error
{
    // {"error":"invalid_client","error_description":"Client Authentication failed"}
    public class PaypalErrorResultDto
    {
        [JsonProperty("error")]
        public string ErrorKey { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }


        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("debug_id")]
        public string DebugId { get; set; }

        [JsonProperty("details")]
        public List<ErrorDetailDto> Details { get; set; }

        [JsonProperty("links")]
        public List<LinkDto> Links { get; set; }

        
    }

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
