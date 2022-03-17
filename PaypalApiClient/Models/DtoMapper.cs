﻿using Apro.Payment.PaypalApiClient.Models.Domain;
using Apro.Payment.PaypalApiClient.Models.Web;
using Apro.Payment.PaypalApiClient.Models.Web.Order.Create;
using Apro.Payment.PaypalApiClient.Models.Web.Order.Get;

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
    }
}