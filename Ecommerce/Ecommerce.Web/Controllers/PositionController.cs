using Ecommerce.Web.Commons;
using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ecommerce.Web.Controllers
{
    public class PositionController : Controller
    {
        private readonly IBaseApiService<Position> _PositionService;
        private readonly IBaseApiService<Menu> _MenuService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPermissionService _permissionService;
        private readonly AppSetting _setting;

        public PositionController(IBaseApiService<Position> PositionService,
           IBaseApiService<Menu> MenuService,
           IPermissionService permissionService,
           IHttpContextAccessor contextAccessor,
           IOptions<AppSetting> options)
        {
            _PositionService = PositionService;
            _permissionService = permissionService;
            _MenuService = MenuService;
            _contextAccessor = contextAccessor;
            _setting = options.Value;
        }
        public IActionResult Index()
        {
            var sessionLogin = _contextAccessor.HttpContext.Session.GetString(Constants.SessionKey.sessionLogin);
            if (sessionLogin == null)
                return RedirectToAction("Login", "Authen");

            return View();
        }

        #region Position
        public async Task<IActionResult> GetListPosition()
        {
            var response = await _PositionService.GetListAsync(_setting.BaseApiUrl + "Position/GetList");
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> _PopUpPosition(string id, string action)
        {
            var model = new PositionViewModel();
            var response = new Response<Position>();

            var listMenu = await _MenuService.GetListAsync(_setting.BaseApiUrl + "Menu/GetListActive");
            if (listMenu.value.Count() > 0)
                ViewBag.listMenu = listMenu.value;

            if (!string.IsNullOrEmpty(id))
                response = await _PositionService.GetAsyncById(_setting.BaseApiUrl + string.Format("Position/Get/{0}", id));

            if (response.value != null)
                model.Position = response.value;

            model.Action = action;
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> SavePosition(Position model, string action)
        {
            var response = new Response<Position>();
            switch (action ?? String.Empty)
            {
                case Constants.Action.Add:
                    response = await _PositionService.InsertAsync(_setting.BaseApiUrl + "Position/Add", model);
                    break;

                case Constants.Action.Update:
                    response = await _PositionService.PutAsync(_setting.BaseApiUrl + "Position/Update", model);
                    break;

                default:
                    break;
            }
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePosition(string id)
        {
            var response = await _PositionService.DeleteAsync(_setting.BaseApiUrl + string.Format("Position/{0}", id));
            return Json(response);
        }

        #endregion

        #region Add Menu to position

        public async Task<IActionResult> GetListMenuPosition()
        {
            var response = await _PositionService.GetListAsync(_setting.BaseApiUrl + "Menu/GetListActive");
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> _PopUpMenuPosition(string id)
        {
            ViewBag.positionId = id;
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> JsTree(string positionId)
        {
            var response = await _permissionService.GetJsTree(_setting.BaseApiUrl + string.Format("Position/GetJsTree/{0}", positionId));
            return Json(response.value);
        }


        [HttpPost]
        public async Task<IActionResult> SaveMenuToPosition(List<Menu> model)
        {
            var response = new Response<List<Menu>>();


            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMenuPosition(string id)
        {
            var response = await _PositionService.DeleteAsync(_setting.BaseApiUrl + string.Format("Position/{0}", id));
            return Json(response);
        }
        #endregion
    }

}
