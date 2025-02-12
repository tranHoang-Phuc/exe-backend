using TuiToot.Server.Api.Dtos.Response;

namespace TuiToot.Server.Api.Services
{
    public interface IBagTypeService
    {
        Task<List<BagTypeResponse>> GetAll();
    }
}
