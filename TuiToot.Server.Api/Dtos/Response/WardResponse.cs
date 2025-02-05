using System.Text.Json.Serialization;

namespace TuiToot.Server.Api.Dtos.Response
{
    public class WardResponse
    {
        [JsonPropertyName("WardCode")]
        public string WardCode { get; set; }
        [JsonPropertyName("DistrictID")]
        public int DistrictId { get; set; }
        [JsonPropertyName("WardName")]
        public string WardName { get; set; }

    }
}
