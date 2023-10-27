using Ecommerce.Web.Models.Authen;
using Ecommerce.Web.Models.Permission;

namespace Ecommerce.Web.Services.Interfaces
{
    public interface ICommonService
    {
        Session GetSessionValue();
        List<DataPermissionJsonInsertList> GetListSessionValue();
    }
}
