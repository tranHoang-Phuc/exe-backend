using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TuiToot.Server.Api.Cores;
using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Services.IServices;

namespace TuiToot.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IDeliveryAddressService _deliveryAddressService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AddressController(IDeliveryAddressService deliveryAddressService, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _deliveryAddressService = deliveryAddressService;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddAddress([FromBody]DeliveryAddressRequest request)
        {
            var response = await _deliveryAddressService.CreateAsync(request);
            var baseResponse = new BaseResponse<DeliveryAddressResponse>
            {
                Data = response
            };
            return Created("", baseResponse);
        }

        [HttpGet("delivery-address")]
        public async Task<IActionResult> GetDeliveryAddress()
        {
            var response = await _deliveryAddressService.GetAllAsync();
            var baseResponse = new BaseResponse<IEnumerable<DeliveryAddressResponse>>
            {
                Data = response
            };
            return Ok(baseResponse);
        }
        [HttpDelete("delivery-address/{addressId}")]
        public async Task<IActionResult> DeleteDeliveryAddress(string addressId)
        {
            await _deliveryAddressService.DeleteAddress(addressId);
            return NoContent();
        }
        [HttpGet("province")]
        public async Task<IActionResult> GetProvince()
        {
            var client =  _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("token", _configuration["GHNApi:Token"]);
            var response = await client.GetAsync(@"https://online-gateway.ghn.vn/shiip/public-api/master-data/province");
            var responseContent = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<BaseResponse<List<ProvinceResponse>>>(responseContent);
            var baseResponse = new BaseResponse<List<ProvinceResponse>>
            {
                Data = data!.Data
            };
            return Ok(baseResponse);
        }
        [HttpGet("district/{provinceId}")]
        public async Task<IActionResult> GetDistrict(string provinceId)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("token", _configuration["GHNApi:Token"]);
            var response = await client.GetAsync($"https://online-gateway.ghn.vn/shiip/public-api/master-data/district?province_id={provinceId}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<BaseResponse<List<DistrictResponse>>>(responseContent);
            var baseResponse = new BaseResponse<List<DistrictResponse>>
            {
                Data = data!.Data
            };
            return Ok(baseResponse);
        }
        [HttpGet("ward/{districtId}")]
        public async Task<IActionResult> GetWard(string districtId)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("token", _configuration["GHNApi:Token"]);
            var response = await client.GetAsync($"https://online-gateway.ghn.vn/shiip/public-api/master-data/ward?district_id={districtId}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<BaseResponse<List<WardResponse>>>(responseContent);
            var baseResponse = new BaseResponse<List<WardResponse>>
            {
                Data = data!.Data
            };
            return Ok(baseResponse);
        }
        [HttpGet("fee/{districtId}")]
        public async Task<IActionResult> GetFee(string districtId)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("token", _configuration["GHNApi:Token"]);
            client.DefaultRequestHeaders.Add("shop_id", _configuration["GHNApi:ShopId"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var queryParams = new Dictionary<string, string>()
            {
                {"service_id", _configuration["GHNApi:ServiceId"] },
                {"insurance_value", _configuration["GHNApi:InsuranceValue"] },
                {"coupon", "" },
                {"to_district_id", districtId },
                {"from_district_id", _configuration["GHNApi:FromDistrictId"] },
                {"weight", _configuration["GHNApi:Weight"] },
                {"length", _configuration["GHNApi:Length"] },
                {"width", _configuration["GHNApi:Width"] },
                {"height",  _configuration["GHNApi:Height"] }
            };

            var url = QueryHelpers.AddQueryString(_configuration["GHNApi:FeeUrl"], queryParams);
            var response = await client.GetAsync(url);
            string jsonData = await response.Content.ReadAsStringAsync();

            var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<ShippingResponse>>(jsonData);
            var baseResponse = new BaseResponse<ShippingResponse>
            {
                Data = deserializedResponse!.Data
            };
            return Ok(baseResponse);
        }
    }
}
