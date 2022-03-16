﻿using Apro.Payment.PaypalApiClient.Models.Domain;
using Apro.Payment.PaypalApiClient.Models.Exceptions;
using Apro.Payment.PaypalApiClient.Models.Web;
using Apro.Payment.PaypalApiClient.Models.Web.Error;
using Apro.Payment.PaypalApiClient.Models.Web.Order.Get;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Apro.Payment.PaypalApiClient.Models
{
    internal class DomainMapper
    {
        internal static PaypalOrder MapOrder(GetOrderDetailsResponseDto responseDto) => new()
        {
            Id = responseDto.Id,
            Status = responseDto.Status,
            Links = new(responseDto.Links?.Select(x => MapLink(x))),
            PurchaseUnits = new PurchaseUnitCollection(responseDto.PurchaseUnits?.Select(x => MapPurchaseUnit(x))),

        };

        internal static PaypalExceptionDetail MapDetail(ErrorDetailDto x)
        {
            return new()
            {
                Description = x.Description,
                Field = x.Field,
                Issue = x.Issue,
                Location = x.Location,
                Value = x.Value
            };
        }

        internal static Link MapLink(LinkDto x) => new Link
        {
            Href = x.Href,
            Method = x.Method,
            Rel = x.Rel,
        };

        internal static PurchaseUnit MapPurchaseUnit(PurchaseUnitDto purchaseUnit)
        {
            return new PurchaseUnit(purchaseUnit.ReferenceId, MapAmount(purchaseUnit.Amount), MapCaptures(purchaseUnit.Payments?.Captures));
        }

        internal static IEnumerable<PaymentCapture> MapCaptures(IEnumerable<CaptureDto> captures)
            => captures?.Select(x => new PaymentCapture() { Id = x.Id, Status = x.Status, Amount = MapAmount(x.Amount) });

        internal static Currency MapAmount(Web.CurrencyDto amount)
        {
            return new Currency(amount?.Value ?? "0", amount?.CurrencyCode ?? "EUR");
        }
    }
}
