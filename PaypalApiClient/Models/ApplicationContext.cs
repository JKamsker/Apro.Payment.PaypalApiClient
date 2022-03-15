
using PaypalPaymentProvider.Models.Order.Create;

using System;

namespace PaypalPaymentProvider.Models
{
    public class ApplicationContext
    {
        public Uri ReturnUrl { get; set; }



        public Uri CancelUrl { get; set; }

        public ApplicationContext()
        {

        }

        public ApplicationContext(Uri returnUrl, Uri cancelUrl)
        {
            ReturnUrl = returnUrl;
            CancelUrl = cancelUrl;
        }

        public ApplicationContext(string returnUrl, string cancelUrl)
            : this(new Uri(returnUrl), new Uri(cancelUrl))
        {

        }

        internal ApplicationContextDto AsDto() => new ApplicationContextDto
        {
            ReturnUrl = ReturnUrl,
            CancelUrl = CancelUrl,
        };
    }
}
