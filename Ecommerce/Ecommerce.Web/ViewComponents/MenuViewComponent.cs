using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models;
using Ecommerce.Web.Models.Menu;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ecommerce.Web.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IBaseApiService<Menu> _baseApiService;
        private readonly ICommonService _commonService;
        private readonly AppSetting _setting;

        public MenuViewComponent(IBaseApiService<Menu> baseApiService, ICommonService commonService,
            IOptions<AppSetting> options)
        {
            _baseApiService = baseApiService;
            _commonService = commonService;
            _setting = options.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = new Response<List<Menu>>();
            var session = _commonService.GetSessionValue();
            if (session != null)
                menu = await _baseApiService.GetListAsync(_setting.BaseApiUrl + string.Format("Menu/{0}", session.positionId));
            else
                menu.value = new List<Menu>();

            return View(menu.value);
        }
    }
}
