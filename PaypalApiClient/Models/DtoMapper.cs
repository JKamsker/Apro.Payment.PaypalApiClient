using Apro.Payment.PaypalApiClient.Models.Domain;
using Apro.Payment.PaypalApiClient.Models.Web;
using Apro.Payment.PaypalApiClient.Models.Web.Order.Create;
using Apro.Payment.PaypalApiClient.Models.Web.Order.Get;
using Apro.Payment.PaypalApiClient.Models.Web.Payment.Refund;

namespace Apro.Payment.PaypalApiClient.Models
{
    public class DtoMapper
    {
        public static ApplicationContextDto MapApplicationContext(ApplicationContext applicationContext) => new ApplicationContextDto
        {
            ReturnUrl = applicationContext.ReturnUrl,
            CancelUrl = applicationContext.CancelUrl,
        };

        internal static PurchaseUnitDto MapPurchaseUnit(PurchaseUnit purchaseUnit)
         => new PurchaseUnitDto(purchaseUnit.ReferenceId, MapAmount(purchaseUnit.Amount));

        internal static CurrencyDto MapAmount(Currency amount)
             => new CurrencyDto(amount.Value, amount.CurrencyCode);

        internal static PaymentRefundRequestDto MapRefundParams(RefundParams refundParams) => new PaymentRefundRequestDto()
        {
            InvoiceId = refundParams.InvoiceId,
            Amount = MapAmount(refundParams.Amount),
            NoteToPayer = refundParams.NoteToPayer,
        };
    }
}
