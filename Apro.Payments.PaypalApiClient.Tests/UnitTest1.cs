using Apro.Payment.PaypalApiClient.Services;

using System;
using System.Threading.Tasks;

using Xunit;

namespace Apro.Payments.PaypalApiClient.Tests
{
    internal class StaticDateTimeProvider : IDateTimeProvider
    {
        public StaticDateTimeProvider(DateTimeOffset dateTimeOffset)
        {
            CurrentDatetimeOffset = dateTimeOffset;
        }

        public DateTime CurrentDatetime => CurrentDatetimeOffset.DateTime;
        public DateTimeOffset CurrentDatetimeOffset { get; set; }
    }

    public class InMemoryCredentialStorageTest
    {
        [Fact]
        public async Task TestExpiry()
        {
            var dateTimeProvider = new StaticDateTimeProvider(new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero));
            var storage = new InMemoryCredentialStorage(dateTimeProvider);
            var creds = new Payment.PaypalApiClient.Models.Domain.PaypalCredentials()
            {
                UserName = "Dummy",
                Secret = "Dummy"
            };
            await storage.SetCredentialsAsync(creds, new PaypalSessionCredentials { AccessToken = "", ExpiresAt = dateTimeProvider.CurrentDatetimeOffset.AddHours(8) });

            Assert.NotNull(await storage.GetCredentialsAsync(creds));

            dateTimeProvider.CurrentDatetimeOffset = dateTimeProvider.CurrentDatetimeOffset + TimeSpan.FromHours(8) - TimeSpan.FromMinutes(4);

            Assert.Null(await storage.GetCredentialsAsync(creds));

        }
    }
}