using Apro.Payment.PaypalApiClient.Models.Web.Order.Create;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Apro.Payment.PaypalApiClient.Models.Domain
{
    public class PurchaseUnit
    {
        public string ReferenceId { get; set; }
        public string Description { get; set; }

        public Currency Amount { get; set; }
        public ICollection<PaymentCapture> Captures { get;  set; }

        public PurchaseUnit(Currency amount)
        {
            Amount = amount;
        }
    }

    public class Currency
    {
        public string CurrencyCode { get; set; }

        public decimal Value { get; set; }

        //internal Currency(string value, string currencyCode)
        //{
        //    Value = value;
        //    CurrencyCode = currencyCode;
        //}

        public Currency(decimal value, string currencyCode)
        {
            Value = value;//value.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("EN-US"));
            CurrencyCode = currencyCode;
        }

        public static Currency Euro(decimal value) => new Currency(value, "EUR");
        public static Currency UsDollar(decimal value) => new Currency(value, "USD");
    }

    public class PaymentCapture
    {
        public string Id { get; set; }

        public PaypalCaptureStatus Status { get; set; }

        public Currency Amount { get; set; }
    }

}
