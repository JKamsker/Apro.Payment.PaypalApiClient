using Ardalis.GuardClauses;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PaypalPaymentProvider.Extensions
{
    public static class GuardExtensions
    {
        public static void Empty<T>(this IGuardClause guardClause, /*[NotNull]*/[ValidatedNotNull] IEnumerable<T> enumerable, /*[CallerArgumentExpression("enumerable")]*/ string parameterName = "", string? message = null)
        {
            if (!guardClause.Null(enumerable, parameterName, message).Any())
            {
                throw new EnumerableEmptyException(parameterName, message);
            }
        }
    }
}
