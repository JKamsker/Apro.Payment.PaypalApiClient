
using System;

namespace Apro.Payment.PaypalApiClient.Services
{
    public interface IDateTimeProvider
    {
        DateTime CurrentDatetime { get; }
        DateTimeOffset CurrentDatetimeOffset { get; }
    }
}
