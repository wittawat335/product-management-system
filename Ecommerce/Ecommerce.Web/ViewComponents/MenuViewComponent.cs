using Ecommerce.Web.Commons;
using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models;
using Ecommerce.Web.Models.Authen;
using Ecommerce.Web.Models.Menu;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace Ecommerce.Web.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IBaseApiService<Menu> _baseApiService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICommonService _commonService;
        private readonly AppSetting _setting;

        public MenuViewComponent(IBaseApiService<Menu> baseApiService,
            ICommonService commonService,
            IOptions<AppSetting> options,
            IHttpContextAccessor contextAccessor)
        {
            _baseApiService = baseApiService;
            _commonService = commonService;
            _setting = options.Value;
            _contextAccessor = contextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = new Response<List<Menu>>();
            var session = _commonService.GetSessionValue();
            if (session.userId != "")
                menu = await _baseApiService.GetListAsync(_setting.BaseApiUrl + string.Format("Menu/{0}", session.positionId));

            return View(menu.value);
        }
    }
}
