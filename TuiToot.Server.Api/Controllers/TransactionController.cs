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
        public async Task<IActionResult> GetAll()
        {
            var response = await _transactionService.GetAll();
            var baseResponse = new BaseResponse<IEnumerable<TransactionResponse>>
            {
                Data = response
            };
            return Ok(baseResponse);
        }
        [HttpGet("{orderIdSearchString}")]
        public async Task<IActionResult> Search([FromRoute]string orderIdSearchString)
        {
            var response = await _transactionService.SearchByOrderId(orderIdSearchString);
            var baseResponse = new BaseResponse<IEnumerable<TransactionResponse>>
            {
                Data = response
            };
            return Ok(baseResponse);
        }
    }
}
