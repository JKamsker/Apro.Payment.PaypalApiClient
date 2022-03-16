using System;

namespace Apro.Payment.PaypalApiClient.Models.Domain
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
    }
}
