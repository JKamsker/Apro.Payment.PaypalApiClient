using Apro.Payment.PaypalApiClient.Models.Domain;
using Apro.Payment.PaypalApiClient.Models.Web.Error;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apro.Payment.PaypalApiClient.Models.Exceptions
{
    [Serializable]
    public class PaypalRequestException : Exception
    {
        public string Name { get; }

        public string DebugId { get; }

        public IReadOnlyList<PaypalExceptionDetail> Details { get; }
        public IReadOnlyList<Link> HelpLinks { get; }


        public PaypalRequestException(PaypalErrorResultDto errorResult) : base(GetMessage(errorResult))
        {
            Name = string.IsNullOrEmpty(errorResult.Name) ? errorResult.ErrorKey : errorResult.Name;
            DebugId = errorResult.DebugId;
            Details = errorResult.Details?.Select(x => DomainMapper.MapDetail(x)).ToList();
            HelpLinks = errorResult.Links?.Select(x => DomainMapper.MapLink(x)).ToList();
        }

        private static string GetMessage(PaypalErrorResultDto errorResult)
        {
            if (!string.IsNullOrEmpty(errorResult.ErrorDescription))
            {
                return errorResult.ErrorDescription;
            }

            if (errorResult.Details?.Count == 1)
            {
                var details = errorResult.Details.First();
                if (!string.IsNullOrEmpty(details.Description))
                {
                    return details.Description;
                }

                //var sb = new StringBuilder();

                //if (!string.IsNullOrEmpty(details.Issue))
                //{
                //    sb.Append(details.Issue);
                //    //return details.Issue;
                //}

                //if (!string.IsNullOrEmpty(details.Description))
                //{
                //    if (sb.Length != 0)
                //    {
                //        sb.Append(": ");
                //    }

                //    sb.Append(details.Description);
                //}

                //return sb.ToString();
            }

            return errorResult.Message;
        }

        public override string ToString()
        {
            return $"[{Name}] {Message}";
        }
    }

    public class PaypalExceptionDetail
    {
        public string Issue { get; set; }
        public string Description { get; set; }


        public string Field { get; set; }
        public string Value { get; set; }
        public string Location { get; set; }
    }
}
