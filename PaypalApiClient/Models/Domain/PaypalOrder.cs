
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Apro.Payment.PaypalApiClient.Models.Domain
{
    public class PaypalOrder
    {
        public string Id { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaypalOrderStatus Status { get; set; }

        public LinkCollection Links { get; set; }

        public PurchaseUnitCollection PurchaseUnits { get; set; }

        public PaypalOrder()
        {

        }
    }



    public class PurchaseUnitCollection : IReadOnlyCollection<PurchaseUnit>
    {
        private readonly ICollection<PurchaseUnit> _purchaseUnits;

        public PurchaseUnitCollection(IEnumerable<PurchaseUnit> purchaseUnits)
        {
            _purchaseUnits = (purchaseUnits ?? Enumerable.Empty<PurchaseUnit>())?.ToList();
        }

        public int Count => _purchaseUnits.Count();

        public IEnumerator<PurchaseUnit> GetEnumerator()
            => _purchaseUnits.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => _purchaseUnits.GetEnumerator();

        public PurchaseUnit FindByReference(string referenceId)
            => _purchaseUnits.FirstOrDefault(x => string.Equals(x.ReferenceId, referenceId, StringComparison.OrdinalIgnoreCase));
    }


    public class LinkCollection : IReadOnlyCollection<Link>
    {
        public Link Self => GetLink("self");
        public Link Approve => GetLink("approve");
        public Link Update => GetLink("update");
        public Link Capture => GetLink("capture");

        public LinkCollection(IEnumerable<Link> links)
        {
            _links = links.ToList();
        }


        private Link GetLink(string name) => _links?.FirstOrDefault(x => string.Equals(x.Rel, name, StringComparison.OrdinalIgnoreCase));

        private readonly ICollection<Link> _links;
        public int Count => _links.Count;

        public IEnumerator<Link> GetEnumerator() => _links.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _links.GetEnumerator();
    }



    public enum PaypalCaptureStatus
    {
        Default,

        /// <summary>
        /// The funds for this captured payment were credited to the payee's PayPal account.
        /// </summary>
        [EnumMember(Value = "COMPLETED")]
        Completed,

        /// <summary>
        /// The funds could not be captured.
        /// </summary>
        [EnumMember(Value = "DECLINED")]
        Declined,

        /// <summary>
        /// An amount less than this captured payment's amount was partially refunded to the payer.
        /// </summary>
        [EnumMember(Value = "PARTIALLY_REFUNDED")]
        PartiallyRefunded,

        /// <summary>
        /// The funds for this captured payment was not yet credited to the payee's PayPal account. 
        /// For more information, see status.details.
        /// </summary>
        [EnumMember(Value = "PENDING")]
        Pending,

        /// <summary>
        /// An amount greater than or equal to this captured payment's amount was refunded to the payer.
        /// </summary>
        [EnumMember(Value = "REFUNDED")]
        Refunded,

        /// <summary>
        /// There was an error while capturing payment.
        /// </summary>
        [EnumMember(Value = "FAILED")]
        Failed
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
}
