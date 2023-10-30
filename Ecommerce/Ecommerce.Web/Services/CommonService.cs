using Ecommerce.Web.Commons;
using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models.Authen;
using Ecommerce.Web.Models.Permission;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Ecommerce.Web.Services
{
    public class CommonService : ICommonService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppSetting _setting;
        HttpClientHandler _httpClient = new HttpClientHandler();


        public CommonService(IHttpContextAccessor contextAccessor, IOptions<AppSetting> options)
        {
            _httpClient.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _contextAccessor = contextAccessor;
            _setting = options.Value;
        }

        public List<Permission> GetListPermissionFromSession()
        {
            var list = new List<Permission>();
            string session = _contextAccessor.HttpContext.Session.GetString(Constants.SessionKey.permission);
            if (session != null)
                list = JsonConvert.DeserializeObject<List<Permission>>(session);
            return list;
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

        public bool IsPermission(string menuId, string actId)
        {
            bool isPermission = false;
            var session = GetListPermissionFromSession();
            if (session.Count() > 0)
            {
                var query = session.Where(x => x.MenuId == menuId && x.ActId == actId).ToList();
                if (query.Count() > 0)
                    isPermission = true;
            }

            return isPermission;
        }
    }
}
