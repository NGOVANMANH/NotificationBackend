using System.Net;

namespace notify.Exceptions
{
    public class GlobalException : Exception
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public GlobalException(string message) : base(message)
        {
        }
        public GlobalException(string message, HttpStatusCode statusCode) : base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}