using System.Text.Json.Serialization;

namespace TuiToot.Server.Api.Cores
{
    public class BaseResponse<T>
    {
        public int Code { get; set; } = 1000;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T Data { get; set; }
    }
}
