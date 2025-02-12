using System.Threading.Tasks;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Infrastructure.EfCore.DataAccess;

namespace TuiToot.Server.Api.Services
{
    public class BagTypeService : IBagTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BagTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<BagTypeResponse>> GetAll()
        {
            var bagTypes = await _unitOfWork.BagTypeRepository.GetAllAsync();
            return bagTypes.Select(b => new BagTypeResponse
            {
                Id = b.Id,
                Name = b.Name,
                Url = b.Url,
                UnitsInStock = b.UnitsInStock,
                Price = b.Price,
                Description = b.Description
            }).ToList();
        }
    }
}
