using System.Net;

namespace Oiga.Common.Exceptions
{
    public class NotFoundException : OigaException
    {
        public NotFoundException(int code, string message) : base(code, message, HttpStatusCode.NotFound)
        {
        }
    }
}
