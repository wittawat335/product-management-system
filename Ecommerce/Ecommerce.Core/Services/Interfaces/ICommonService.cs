using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface ICommonService
    {
        string Encrypt(string text);
        string Decrypt(string text);
        string GetPositionName(string id);
        string GetParameter(string code);
        string GetMenuDefault(string menuId);
        List<DataPermissionJsonInsertList> GetListPermissionFromSession();
    }
}
