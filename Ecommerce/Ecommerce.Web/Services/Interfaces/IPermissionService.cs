using Ecommerce.Web.Models;
using Ecommerce.Web.Models.Permission;

namespace Ecommerce.Web.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<Response<List<TreeViewNode>>> GetJsTree(string path);
        Response<List<DataPermissionJsonInsertList>> SetPermissionToSession(List<DataPermissionJsonInsertList> permissionData);
        Task Save(string positionId, List<DataPermissionJsonInsertList> model);
        Task<Response<Permission>> SavePermission(string positionId);
    }
}
