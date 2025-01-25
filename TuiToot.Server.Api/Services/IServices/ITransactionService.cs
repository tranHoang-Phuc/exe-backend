using TuiToot.Server.Api.Dtos.Response;

namespace TuiToot.Server.Api.Services.IServices
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionResponse>> GetAll();
        Task<IEnumerable<TransactionResponse>> SearchByOrderId(string orderId);
    }
}
