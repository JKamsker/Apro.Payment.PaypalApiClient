
using System;

namespace PaypalPaymentProvider.Services
{
    public interface IDateTimeProvider
    {
        DateTime CurrentDatetime { get; }
        DateTimeOffset CurrentDatetimeOffset { get; }
    }
}
