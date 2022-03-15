using System;
using System.Runtime.Serialization;

namespace PaypalPaymentProvider.Extensions
{
    [Serializable]
    internal class EnumerableEmptyException : Exception
    {
        public string ParameterName { get; }

        public EnumerableEmptyException()
        {

        }

        public EnumerableEmptyException(string parameterName, string? message = null) 
            : base(!string.IsNullOrEmpty(message) ? message : String.IsNullOrEmpty(parameterName) ? "Parameter cannot be empty" : $"Parameter '{parameterName}' cannot be empty")
        {
            ParameterName = parameterName;
        }



        protected EnumerableEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}