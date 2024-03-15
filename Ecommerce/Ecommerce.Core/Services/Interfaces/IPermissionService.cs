using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        Task DeleteListByPositionId(string positionId);
        Task<Response<Permission>> Add(PermissionDTO model);
        Task<Response<string>> SaveList(string id, List<DataPermissionJsonInsertList> permissionData);
        Task<Response<List<PermissionDTO>>> GetList(string positionId);
        Task childrenList(string positionId, List<DataPermissionJsonInsertList> model);
        Task<Response<List<DataPermissionJsonList>>> GetListJsTree(string positionId);
    }
}
