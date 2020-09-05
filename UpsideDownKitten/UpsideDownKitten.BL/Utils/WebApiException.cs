using System;
using System.Runtime.Serialization;

namespace UpsideDownKitten.BL.Utils
{
    public class WebApiException : Exception
    {
        public WebApiException()
        {
        }

        protected WebApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public WebApiException(string message) : base(message)
        {
        }

        public WebApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
