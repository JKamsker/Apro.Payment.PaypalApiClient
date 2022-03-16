using Apro.Payment.PaypalApiClient.Models.Web.Order;
using Apro.Payment.PaypalApiClient.Models.Web.Payment.Refund;

using System;
using System.Collections.Generic;
using System.Text;

namespace Apro.Payment.PaypalApiClient.Models.Domain
{
    public class PaypalRefund
    {
        public string Id { get; set; }

        public string Reason { get; set; }
        public PaypalRefundStatus Status { get; set; }

        public Currency Amount { get; set; }
    }
}
