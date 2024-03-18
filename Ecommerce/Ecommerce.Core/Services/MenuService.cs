using AutoMapper;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.RepositoryContracts;

namespace Ecommerce.Core.Services
{
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<Menu> _MenuRepository;
        private readonly IGenericRepository<Permission> _PermissionRepository;
        private readonly IStoredRespository _storedRespository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Menu> MenuRepository, IGenericRepository<Permission> PermissionRepository,
            IStoredRespository storedRespository, IMapper mapper)
        {
            _MenuRepository = MenuRepository;
            _PermissionRepository = PermissionRepository;
            _storedRespository = storedRespository;
            _mapper = mapper;
        }

        public async Task<Response<List<MenuDTO>>> GetListByPermission(string positionId)
        {
            var response = new Response<List<MenuDTO>>();
            IQueryable<Permission> tbPermission = await _PermissionRepository.AsQueryable(x => x.PositionId == positionId);
            IQueryable<Menu> tbMenu = await _MenuRepository.AsQueryable();
            IQueryable<Menu> tbResult = (from p in tbPermission
                                         join m in tbMenu on p.MenuId equals m.MenuId
                                         select m).AsQueryable();

            var listMenus = tbResult.Distinct();
            if (listMenus.Count() > 0)
            {
                response.value = _mapper.Map<List<MenuDTO>>(listMenus);
                response.isSuccess = Constants.Status.True;
                response.message = Constants.StatusMessage.Success;
            }

            return response;
        }

        public async Task<Response<List<MenuDTO>>> GetListMenuActive()
        {
            var response = new Response<List<MenuDTO>>();
            var query = await _MenuRepository.GetListAsync(x => x.Url != null && x.Status == Constants.Status.Active);
            if (query.Count() > 0)
            {
                response.value = _mapper.Map<List<MenuDTO>>(query);
                response.isSuccess = Constants.Status.True;
            }

            return response;
        }

        public async Task<Response<List<MenuDTO>>> GetMenuByPositionId(string positionId)
        {
            var response = new Response<List<MenuDTO>>();
            var menu = new List<Menu>();
            if (positionId == "Dev01")
            {
                menu = await _MenuRepository.GetListAsync(x => x.Status == Constants.Status.Active);
            }
            else
            {
                menu = await _storedRespository.SP_GET_MENU_BY_POSITION(positionId);
            }
            if (menu.Count() > 0)
            {
                response.value = _mapper.Map<List<MenuDTO>>(menu);
                response.isSuccess = Constants.Status.True;
                response.message = Constants.StatusMessage.Success;
            }

            return response;
        }
    }
}
