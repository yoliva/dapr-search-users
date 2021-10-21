using System;
using System.Net;

namespace Oiga.Common.Exceptions
{
    public abstract class OigaException : Exception
    {
        public OigaException(int errorCode, string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }

        public int ErrorCode { get; }
        public HttpStatusCode StatusCode { get; }
    }
}
