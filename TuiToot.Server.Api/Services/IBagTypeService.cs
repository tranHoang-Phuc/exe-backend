using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;

namespace TuiToot.Server.Api.Services
{
    public interface IBagTypeService
    {
        Task<List<BagTypeResponse>> GetAll();
        Task<BagTypeResponse> Create(BagTypeCreation bagTypeCreation);
        Task<bool> Delete(string id);
    }
}
