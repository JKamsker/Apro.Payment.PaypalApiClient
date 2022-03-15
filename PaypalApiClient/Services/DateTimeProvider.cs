
using System;

namespace PaypalPaymentProvider.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset CurrentDatetimeOffset => DateTimeOffset.UtcNow;
        public DateTime CurrentDatetime => DateTime.UtcNow;
    }
}
