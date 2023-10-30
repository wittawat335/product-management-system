using Ecommerce.Web.Models;
using Ecommerce.Web.Models.Authen;
using Ecommerce.Web.Models.Permission;

namespace Ecommerce.Web.Services.Interfaces
{
    public interface ICommonService
    {
        Session GetSessionValue();
        List<Permission> GetListPermissionFromSession();
        List<DataPermissionJsonInsertList> GetListSessionValue();
        bool IsPermission(string menuId, string actId);
    }
}
