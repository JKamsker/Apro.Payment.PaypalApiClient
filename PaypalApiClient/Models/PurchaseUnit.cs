using PaypalPaymentProvider.Models.Order.Create;

using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PaypalPaymentProvider.Models
{
    public  class PurchaseUnit
    {
        public string ReferenceId { get; set; }

        public Currency Amount { get; set; }

        public PurchaseUnit(string referenceId, Currency amount)
        {
            ReferenceId = referenceId;
            Amount = amount;
        }

        internal PurchaseUnitDto AsDto() 
            => new PurchaseUnitDto(ReferenceId, Amount.AsDto());
    }

    public class Currency
    {
        public string CurrencyCode { get; set; }

        public string Value { get; set; }

        public Currency(decimal value, string currencyCode)
        {
            Value = value.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("EN-US"));
            CurrencyCode = currencyCode;
        }

        public static Currency Euro(decimal value) => new Currency(value, "EUR");
        public static Currency UsDollar(decimal value) => new Currency(value, "USD");

        internal CurrencyDto AsDto()
            => new CurrencyDto(Value, CurrencyCode);
    }

}
