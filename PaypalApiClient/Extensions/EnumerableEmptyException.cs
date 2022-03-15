
using System;
using System.Runtime.Serialization;

namespace Apro.Payment.PaypalApiClient.Extensions
{
    [Serializable]
    internal class EnumerableEmptyException : Exception
    {
        public string ParameterName { get; }

        public EnumerableEmptyException()
        {

        }

        public EnumerableEmptyException(string parameterName, string message = null)
            : base(!string.IsNullOrEmpty(message) ? message : string.IsNullOrEmpty(parameterName) ? "Parameter cannot be empty" : $"Parameter '{parameterName}' cannot be empty")
        {
            ParameterName = parameterName;
        }



        protected EnumerableEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}