using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;

namespace TuiToot.Server.Api.Services.IServices
{
    public interface IAvalibleProductService
    {
        Task<IEnumerable<AvalibleProductResponse>> GetAll();
        Task<AvalibleProductResponse> GetById(string id);
        Task<AvalibleProductResponse> Update(string id, UpdatedAvaliableProduct data);
        Task<AvalibleProductResponse> Create(AvalibleProductCreation avalibleProductCreation);
        Task<bool> Delete(string id);
    }
}
