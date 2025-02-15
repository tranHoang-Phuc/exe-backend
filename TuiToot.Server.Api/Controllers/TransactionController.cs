using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuiToot.Server.Api.Cores;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Services.IServices;

namespace TuiToot.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _transactionService.GetAll();
            var baseResponse = new BaseResponse<IEnumerable<TransactionResponse>>
            {
                Data = response
            };
            return Ok(baseResponse);
        }
        [HttpGet("Order/{orderIdSearchString}")]
        public async Task<IActionResult> Search([FromRoute]string orderIdSearchString)
        {
            var response = await _transactionService.SearchByOrderId(orderIdSearchString);
            var baseResponse = new BaseResponse<IEnumerable<TransactionResponse>>
            {
                Data = response
            };
            return Ok(baseResponse);
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> Get([FromRoute] string transactionId)
        {
            var response = await _transactionService.GetById(transactionId);
            var baseResponse = new BaseResponse<TransactionResponse>
            {
                Data = response
            };
            return Ok(baseResponse);
        }

        [HttpGet("order/search/{orderId}")]
        
        public async Task<IActionResult> GetByOrderId(string orderId) 
        { 
            var response = await _transactionService.GetByOrderId(orderId);
            var baseResponse = new BaseResponse<TransactionResponse>
            {
                Data = response
            };
            return Ok(baseResponse);
        }

        [HttpGet("search-transaction/{transactionId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetByTransactionId(string transactionId)
        {
            var response = await _transactionService.SearchById(transactionId);
            var baseResponse = new BaseResponse<List<TransactionResponse>>
            {
                Data = response
            };
            return Ok(baseResponse);
        }
    }
}
