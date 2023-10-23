using Ecommerce.Web.Commons;
using Ecommerce.Web.Models;
using Ecommerce.Web.Models.Authen;
using Ecommerce.Web.Services.Interfaces;
using Newtonsoft.Json;

namespace Ecommerce.Web.Services
{
    public class CommonService : ICommonService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CommonService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public List<DataPermissionJsonInsertList> GetListSessionValue()
        {
            List<DataPermissionJsonInsertList> list = new List<DataPermissionJsonInsertList>();
            string session = _contextAccessor.HttpContext.Session.GetString("listSelectedPermission");
            if (session != null)
                list = JsonConvert.DeserializeObject<List<DataPermissionJsonInsertList>>(session);
            return list;
        }

        public Session GetSessionValue()
        {
            Session result = null;
            string sessionString = _contextAccessor.HttpContext.Session.GetString(Constants.SessionKey.sessionLogin);
            if (sessionString != null)
                result = JsonConvert.DeserializeObject<Session>(sessionString);

            return result;
        }

    }
}
