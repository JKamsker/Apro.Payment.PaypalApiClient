using Newtonsoft.Json;

using System.Globalization;

namespace Apro.Payment.PaypalApiClient.Models.Web
{
    public class CurrencyDto
    {
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        internal CurrencyDto(string value, string currencyCode)
        {
            Value = value;
            CurrencyCode = currencyCode;
        }
        public CurrencyDto(decimal value, string currencyCode)
            : this(value.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("EN-US")), currencyCode)
        {
        }

        public static CurrencyDto Euro(decimal value) => new CurrencyDto(value, "EUR");
        public static CurrencyDto UsDollar(decimal value) => new CurrencyDto(value, "USD");
    }
}
