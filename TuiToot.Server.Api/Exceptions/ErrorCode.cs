using System.Net;

namespace TuiToot.Server.Api.Exceptions
{
    public class ErrorCode
    {
        public int Code { get; }
        public string Message { get; }
        public HttpStatusCode StatusCode { get; }

        public ErrorCode(int code, string message, HttpStatusCode statusCode)
        {
            Code = code;
            Message = message;
            StatusCode = statusCode;
        }
        public static readonly ErrorCode UncategorizedException = new ErrorCode(9999, "Uncategorized error", HttpStatusCode.InternalServerError);
        public static readonly ErrorCode InvalidKey = new ErrorCode(1001, "Invalid Key Exception", HttpStatusCode.BadRequest);
    }
   
}

