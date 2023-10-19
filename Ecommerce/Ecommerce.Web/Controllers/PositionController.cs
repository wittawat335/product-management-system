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
        private readonly AppSetting _setting;

        public PositionController(IBaseApiService<Position> PositionService,
           IBaseApiService<Menu> MenuService,
           IHttpContextAccessor contextAccessor,
           IOptions<AppSetting> options)
        {
            _PositionService = PositionService;
            _MenuService = MenuService;
            _contextAccessor = contextAccessor;
            _setting = options.Value;
        }
        public IActionResult Index()
        {
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
            try
            {
                var listMenu = await _MenuService.GetListAsync(_setting.BaseApiUrl + "Menu/GetListActive");
                if (listMenu.value.Count() > 0)
                    ViewBag.listMenu = listMenu.value;

                if (!string.IsNullOrEmpty(id))
                    response = await _PositionService.GetAsyncById(_setting.BaseApiUrl + string.Format("Position/Get/{0}", id));

                if (response.value != null)
                    model.Position = response.value;

                model.Action = action;
            }
            catch
            {
                throw;
            }

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> SavePosition(Position model)
        {
            var response = new Response<Position>();
            try
            {
                if (model != null)
                {
                    switch (model.PositionId ?? String.Empty)
                    {
                        case "":
                            response = await _PositionService.InsertAsync(_setting.BaseApiUrl + "Position/Add", model);
                            break;

                        default:
                            response = await _PositionService.PutAsync(_setting.BaseApiUrl + "Position/Update", model);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
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
        public async Task<IActionResult> _PopUpMenuPosition()
        {
            var model = new MenuViewModel();
            var response = new Response<List<Menu>>();

            response = await _MenuService.GetListAsync(_setting.BaseApiUrl + "Menu/GetListActive");
            if (response.value != null)
                model.listMenu = response.value;

            return PartialView(model);
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
