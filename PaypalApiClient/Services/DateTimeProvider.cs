
using System;

namespace Apro.Payment.PaypalApiClient.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset CurrentDatetimeOffset => DateTimeOffset.UtcNow;
        public DateTime CurrentDatetime => DateTime.UtcNow;
    }
}
