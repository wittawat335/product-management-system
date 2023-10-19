using Ecommerce.Web.Models;

namespace Ecommerce.Web.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<Response<List<TreeViewNode>>> GetJsTree(string path);
    }
}
