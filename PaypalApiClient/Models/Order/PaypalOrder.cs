using Apro.Payment.PaypalApiClient.Models.Order.Capture;
using Apro.Payment.PaypalApiClient.Models.Order.Create;
using Apro.Payment.PaypalApiClient.Models.Order.Get;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Apro.Payment.PaypalApiClient.Models.Order
{
    public class PaypalOrder
    {
        public string Id { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaypalOrderStatus Status { get; set; }

        public LinkCollection Links { get; set; }

        public PaypalOrder()
        {

        }

        internal static PaypalOrder FromDto(PaypalCreateOrderResponseDto responseDto) => new()
        {
            Id = responseDto.Id,
            Status = responseDto.Status,
            Links = new(responseDto.Links)
        };

        internal static PaypalOrder FromDto(GetOrderDetailsResponseDto responseDto) => new()
        {
            Id = responseDto.Id,
            Status = responseDto.Status,
            Links = new(responseDto.Links)
        };

        internal static PaypalOrder FromDto(CaptureOrderResponseDto responseDto) => new()
        {
            Id = responseDto.Id,
            Status = responseDto.Status,
            Links = new(responseDto.Links)
        };
    }

    public class LinkCollection : IReadOnlyCollection<Link>
    {
        public Link Self => GetLink("self");
        public Link Approve => GetLink("approve");
        public Link Update => GetLink("update");
        public Link Capture => GetLink("capture");

        public LinkCollection(ICollection<Link> links)
        {
            _links = links;
        }


        private Link GetLink(string name) => _links?.FirstOrDefault(x => string.Equals(x.Rel, name, StringComparison.OrdinalIgnoreCase));

        private readonly ICollection<Link> _links;
        public int Count => _links.Count;

        public IEnumerator<Link> GetEnumerator() => _links.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _links.GetEnumerator();
    }

    public enum PaypalOrderStatus
    {
        Default,

        /// <summary>
        /// The order was created with the specified context.
        /// </summary>
        [EnumMember(Value = "CREATED")]
        Created,

        /// <summary>
        /// The order was saved and persisted. 
        /// The order status continues to be in progress until a capture is made with final_capture = true for all purchase units within the order.
        /// </summary>
        [EnumMember(Value = "SAVED")]
        Saved,

        /// <summary>
        /// The customer approved the payment through the PayPal wallet or another form of guest or unbranded payment. 
        /// For example, a card, bank account, or so on.
        /// </summary>
        [EnumMember(Value = "APPROVED")]
        Approved,

        /// <summary>
        /// All purchase units in the order are voided.
        /// </summary>
        [EnumMember(Value = "VOIDED")]
        Voided,

        /// <summary>
        /// The payment was authorized or the authorized payment was captured for the order.
        /// </summary>
        [EnumMember(Value = "COMPLETED")]
        Completed,

        /// <summary>
        /// The order requires an action from the payer (e.g. 3DS authentication). 
        /// Redirect the payer to the "rel":"payer-action" HATEOAS link returned as part of the response prior to authorizing or capturing the order.
        /// </summary>
        [EnumMember(Value = "PAYER_ACTION_REQUIRED")]
        PayerActionRequired
    }
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
