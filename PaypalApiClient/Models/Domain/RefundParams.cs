using System;
using System.Collections.Generic;
using System.Text;

namespace Apro.Payment.PaypalApiClient.Models.Domain
{
    public  class RefundParams
    {
        public string CaptureId { get; set; }

        public Currency Amount { get; set; }

        public string NoteToPayer { get; set; }

        public string InvoiceId { get; set; }

        /// <summary>
        /// Refunds complete paymentCapture
        /// </summary>
        /// <param name="captureId"></param>
        public RefundParams(string captureId)
        {
            CaptureId = captureId;
        }

        public RefundParams(string captureId, Currency amount) : this(captureId)
        {
            Amount = amount;
        }

        public RefundParams WithNote(string note)
        {
            NoteToPayer = note;
            return this;
        }

        public RefundParams WithInvoiceId(string invoiceId)
        {
            InvoiceId = invoiceId;
            return this;
        }
    }
}
