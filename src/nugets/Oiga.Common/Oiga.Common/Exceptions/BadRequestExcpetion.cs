using System.Net;

namespace Oiga.Common.Exceptions
{
    public class BadRequestExcpetion : OigaException
    {
        public BadRequestExcpetion(int code, string message) : base(code, message, HttpStatusCode.BadRequest)
        {
        }
    }
}
