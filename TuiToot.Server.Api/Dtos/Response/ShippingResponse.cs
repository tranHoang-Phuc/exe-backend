using System.Text.Json.Serialization;

namespace TuiToot.Server.Api.Dtos.Response
{
    public class ShippingResponse
    {
        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        [JsonPropertyName("service_fee")]
        public decimal ServiceFee { get; set; }

        [JsonPropertyName("insurance_fee")]
        public decimal InsuranceFee { get; set; }

        [JsonPropertyName("pick_station_fee")]
        public decimal PickStationFee { get; set; }

        [JsonPropertyName("coupon_value")]
        public decimal CouponValue { get; set; }

        [JsonPropertyName("r2s_fee")]
        public decimal R2sFee { get; set; }

        [JsonPropertyName("return_again")]
        public decimal ReturnAgain { get; set; }

        [JsonPropertyName("document_return")]
        public decimal DocumentReturn { get; set; }

        [JsonPropertyName("double_check")]
        public decimal DoubleCheck { get; set; }

        [JsonPropertyName("cod_fee")]
        public decimal CodFee { get; set; }

        [JsonPropertyName("pick_remote_areas_fee")]
        public decimal PickRemoteAreasFee { get; set; }

        [JsonPropertyName("deliver_remote_areas_fee")]
        public decimal DeliverRemoteAreasFee { get; set; }

        [JsonPropertyName("cod_failed_fee")]
        public decimal CodFailedFee { get; set; }

    }
}
