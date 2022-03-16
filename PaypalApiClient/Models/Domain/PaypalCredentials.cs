
using System;

namespace Apro.Payment.PaypalApiClient.Models.Domain
{
    public class PaypalCredentials : IEquatable<PaypalCredentials>
    {
        public string UserName { get; set; }
        public string Secret { get; set; }

        public bool Equals(PaypalCredentials other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(UserName, other.UserName) && string.Equals(UserName, other.UserName);
        }

        public override int GetHashCode() => HashCode.Combine(UserName, Secret);
    }
}
