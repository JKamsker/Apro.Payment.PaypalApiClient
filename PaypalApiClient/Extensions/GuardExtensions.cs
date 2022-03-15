
using Ardalis.GuardClauses;

using System.Collections.Generic;
using System.Linq;

namespace Apro.Payment.PaypalApiClient.Extensions
{
    public static class GuardExtensions
    {
        public static void Empty<T>(this IGuardClause guardClause, /*[NotNull]*/[ValidatedNotNull] IEnumerable<T> enumerable, /*[CallerArgumentExpression("enumerable")]*/ string parameterName = "", string message = null)
        {
            if (!guardClause.Null(enumerable, parameterName, message).Any())
            {
                throw new EnumerableEmptyException(parameterName, message);
            }
        }
    }
}
