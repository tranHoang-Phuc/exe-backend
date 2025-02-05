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
        public static readonly ErrorCode UserExisted = new ErrorCode(1002, "User existed", HttpStatusCode.BadRequest);
        public static readonly ErrorCode Unauthorized = new ErrorCode(1003, "Unauthorized", HttpStatusCode.Unauthorized);
        public static readonly ErrorCode InvalidToken = new ErrorCode(1004, "Invalid Token", HttpStatusCode.BadRequest);
        public static readonly ErrorCode NotFound = new ErrorCode(1005, "Not Found", HttpStatusCode.NotFound);
        public static readonly ErrorCode InvalidFile = new ErrorCode(1006, "Invalid File", HttpStatusCode.BadRequest);
        public static readonly ErrorCode FileNotFound = new ErrorCode(1007, "File Not Found", HttpStatusCode.NotFound);
        public static readonly ErrorCode UploadFailed = new ErrorCode(1008, "Upload Fail", HttpStatusCode.BadRequest);
        public static readonly ErrorCode ConflictData = new ErrorCode(1009, "Data Conflict", HttpStatusCode.Conflict);
    }
   
}

