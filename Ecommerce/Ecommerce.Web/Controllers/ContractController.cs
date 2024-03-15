using Ecommerce.Web.Commons;
using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models.Email;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ecommerce.Web.Controllers
{
    public class ContractController : Controller
    {
        private readonly IBaseApiService<MailRequest> _service;
        private readonly AppSetting _setting;
        public ContractController(IBaseApiService<MailRequest> service, IOptions<AppSetting> options)
        {
            _service = service;
            _setting = options.Value;
        }

        public IActionResult Index()
        {
            var sessionLogin = HttpContext.Session.GetString(Constants.SessionKey.sessionLogin);
            if (sessionLogin == null)
                return RedirectToAction("Login", "Authen");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(MailRequest request)
        {
            var response = await _service.PostAsJsonAsync(_setting.BaseApiUrl + "SendMail", request);
            if (response.returnUrl != null)
                response.returnUrl = Url.Content(response.returnUrl);

            return Json(response);
        }
    }
}
