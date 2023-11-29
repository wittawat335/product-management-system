using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface IPositionService
    {
        Task<Response<List<PositionDTO>>> GetList(PositionDTO filter = null);
        Task<Response<PositionDTO>> Get(string id);
        Task<Response<Position>> Insert(PositionDTO model);
        Task<Response<Position>> Update(PositionDTO model);
        Task<Response<Position>> Delete(string id);
        Task<string> CheckDupilcate(string id);
        Task<Response<List<DataPermissionJsonList>>> GetListPermissionData(string positionId);
    }
}
