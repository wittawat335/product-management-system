using Ecommerce.Web.Commons;
using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models;
using Ecommerce.Web.Models.Position;
using Ecommerce.Web.Models.User;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ecommerce.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IBaseApiService<User> _service;
        private readonly IBaseApiService<Position> _positionService;
        private readonly AppSetting _setting;

        public UserController(IBaseApiService<User> service, IBaseApiService<Position> positionService, IOptions<AppSetting> options)
        {
            _service = service;
            _positionService = positionService;
            _setting = options.Value;
        }
        public IActionResult Index()
        {
            var sessionLogin = HttpContext.Session.GetString(Constants.SessionKey.sessionLogin);
            if (sessionLogin == null)
                return RedirectToAction("Login", "Authen");

            return View();
        }

        public async Task<IActionResult> GetList(User search)
        {
            var response = await _service.Search(_setting.BaseApiUrl + "User/GetList", search);
            if (response.returnUrl != null)
                response.returnUrl = Url.Content(response.returnUrl);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> _PopUpDialog(string id, string action)
        {
            var model = new UserViewModel();
            var response = new Response<User>();

            var listPosition = await _positionService.GetListAsync(_setting.BaseApiUrl + "Position/GetListActive");
            if (listPosition.value.Count() > 0)
                ViewBag.listPosition = listPosition.value;

            if (!string.IsNullOrEmpty(id))
                response = await _service.GetAsyncById(_setting.BaseApiUrl + string.Format("User/GetUser/{0}", id));

            if (response.value != null)
                model.User = response.value;

            model.Action = action;
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(User model, string action)
        {
            var response = new Response<User>();
            switch (action ?? String.Empty)
            {
                case Constants.Action.Add:
                    response = await _service.InsertAsync(_setting.BaseApiUrl + "User/Add", model);
                    break;

                case Constants.Action.Update or "cusUpdate":
                    response = await _service.PutAsync(_setting.BaseApiUrl + "User/Update", model);
                    break;
            }
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.DeleteAsync(_setting.BaseApiUrl + string.Format("User/{0}", id));
            return Json(response);
        }

        public async Task<IActionResult> select2UserName(string query)
        {
            var response = await _service.GetListAsync(_setting.BaseApiUrl + "User/GetList");
            response.value = response.value.Where(x => x.Username.ToLower().Contains(query.ToLower())).ToList();
            return Json(response.value);
        }

        public async Task<IActionResult> select2Position(string query)
        {
            var response = await _service.GetListAsync(_setting.BaseApiUrl + "User/GetList");
            response.value = response.value.Where(x => x.PositionName.ToLower().Contains(query.ToLower())).ToList();
            return Json(response.value);
        }
    }
}
