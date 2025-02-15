using TuiToot.Server.Api.Dtos.Response;

namespace TuiToot.Server.Api.Services.IServices
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionResponse>> GetAll();
        Task<TransactionResponse> GetById(string transactionId);
        Task<TransactionResponse> GetByOrderId(string orderId);
        Task<List<TransactionResponse>> SearchById(string transactionId);
        Task<IEnumerable<TransactionResponse>> SearchByOrderId(string orderId);
    }
}
