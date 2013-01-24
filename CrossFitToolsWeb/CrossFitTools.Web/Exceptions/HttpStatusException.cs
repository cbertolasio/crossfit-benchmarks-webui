using System;
using System.Net;

namespace CrossfitBenchmarks.WebUi.Exceptions
{
    public class HttpStatusException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public HttpStatusException()
            : base()
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public HttpStatusException(HttpStatusCode statusCode)
            : base()
        {
            StatusCode = statusCode;
        }

        public HttpStatusException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusException(HttpStatusCode statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}

