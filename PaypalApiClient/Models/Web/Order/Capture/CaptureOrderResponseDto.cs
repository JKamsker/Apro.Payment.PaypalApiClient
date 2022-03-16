using Apro.Payment.PaypalApiClient.Models.Domain;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;
using System.Collections.Generic;

namespace Apro.Payment.PaypalApiClient.Models.Web.Order.Capture
{
    public class CaptureOrderResponseDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaypalOrderStatus Status { get; set; }

        [JsonProperty("purchase_units")]
        public List<PurchaseUnit> PurchaseUnits { get; set; }

        [JsonProperty("payer")]
        public Payer Payer { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }
    }


    public class Payer
    {
        [JsonProperty("name")]
        public PayerName Name { get; set; }

        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }

        [JsonProperty("payer_id")]
        public string PayerId { get; set; }

        [JsonProperty("address")]
        public PayerAddress Address { get; set; }
    }

    public class PayerAddress
    {
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
    }

    public class PayerName
    {
        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }
    }

    public class PurchaseUnit
    {
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }

        [JsonProperty("shipping")]
        public Shipping Shipping { get; set; }

        [JsonProperty("payments")]
        public Payments Payments { get; set; }
    }

    public class Payments
    {
        [JsonProperty("captures")]
        public List<Capture> Captures { get; set; }
    }

    public class Capture
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("amount")]
        public Amount Amount { get; set; }

        [JsonProperty("final_capture")]
        public bool FinalCapture { get; set; }

        [JsonProperty("seller_protection")]
        public SellerProtection SellerProtection { get; set; }

        [JsonProperty("seller_receivable_breakdown")]
        public SellerReceivableBreakdown SellerReceivableBreakdown { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }

        [JsonProperty("create_time")]
        public DateTimeOffset CreateTime { get; set; }

        [JsonProperty("update_time")]
        public DateTimeOffset UpdateTime { get; set; }
    }

    public class Amount
    {
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class SellerProtection
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("dispute_categories")]
        public List<string> DisputeCategories { get; set; }
    }

    public class SellerReceivableBreakdown
    {
        [JsonProperty("gross_amount")]
        public Amount GrossAmount { get; set; }

        [JsonProperty("paypal_fee")]
        public Amount PaypalFee { get; set; }

        [JsonProperty("net_amount")]
        public Amount NetAmount { get; set; }
    }

    public class Shipping
    {
        [JsonProperty("name")]
        public ShippingName Name { get; set; }

        [JsonProperty("address")]
        public ShippingAddress Address { get; set; }
    }

    public class ShippingAddress
    {
        [JsonProperty("address_line_1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("address_line_2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("admin_area_2")]
        public string AdminArea2 { get; set; }

        [JsonProperty("admin_area_1")]
        public string AdminArea1 { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
    }

    public class ShippingName
    {
        [JsonProperty("full_name")]
        public string FullName { get; set; }
    }
}
