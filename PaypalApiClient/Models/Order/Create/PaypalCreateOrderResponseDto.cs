﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PaypalPaymentProvider.Models.Order.Create
{
    public class PaypalCreateOrderResponseDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaypalOrderStatus Status { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }
    }


}
