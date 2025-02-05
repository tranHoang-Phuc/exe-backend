using System.Text.Json.Serialization;

namespace TuiToot.Server.Api.Dtos.Response
{
    public class ProvinceResponse
    {
        [JsonPropertyName("ProvinceID")]
        public int ProvinceID { get; set; }
        [JsonPropertyName("ProvinceName")]
        public string ProvinceName { get; set; }
        [JsonPropertyName("CountryID")]
        public int CountryID { get; set; }
        [JsonPropertyName("Code")]
        public string Code { get; set; }
        [JsonPropertyName("NameExtension")]
        public List<string> NameExtension { get; set; }

    }
}
