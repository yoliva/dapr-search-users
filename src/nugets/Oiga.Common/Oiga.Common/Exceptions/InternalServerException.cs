using System.Net;

namespace Oiga.Common.Exceptions
{
    class InternalServerException : OigaException
    {
        public InternalServerException(int code, string message) : base(code, message, HttpStatusCode.InternalServerError)
        {
        }
    }
}
