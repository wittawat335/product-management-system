using Ecommerce.Web.Commons;
using Ecommerce.Web.Models;
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

        public Session GetSessionValue()
        {
            var session = new Session();
            string sessionString = _contextAccessor.HttpContext.Session.GetString(Constants.SessionKey.sessionLogin);

            if (sessionString != null)
                session = JsonConvert.DeserializeObject<Session>(sessionString);

            return session;
        }
    }
}
