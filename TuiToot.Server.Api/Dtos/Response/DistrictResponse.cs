using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TuiToot.Server.Api.Dtos.Response
{
    public class DistrictResponse
    {
        [JsonPropertyName("DistrictID")]
        public int DistrictID { get; set; }
        [JsonPropertyName("ProvinceID")]
        public int ProvinceID { get; set; }
        [JsonPropertyName("DistrictName")]
        public string DistrictName { get; set; }
    }
}
