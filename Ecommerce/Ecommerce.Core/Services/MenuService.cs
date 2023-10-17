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
        private readonly IGenericRepository<User> _UserRepository;
        private readonly IGenericRepository<MenuPosition> _MenuPositionRepository;
        private readonly IMapper _mapper;

        public MenuService(
            IGenericRepository<Menu> MenuRepository,
            IGenericRepository<User> UserRepository,
            IGenericRepository<MenuPosition> MenuPositionRepository,
            IMapper mapper)
        {
            _MenuRepository = MenuRepository;
            _UserRepository = UserRepository;
            _MenuPositionRepository = MenuPositionRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<MenuDTO>>> GetListMenuActive()
        {
            var response = new Response<List<MenuDTO>>();
            try
            {
                response.value = _mapper.Map<List<MenuDTO>>
                    (await _MenuRepository.GetListAsync(x => x.Url != null && x.Status == Constants.Status.Active));

                if (response.value.Count() > 0)
                    response.isSuccess = Constants.Status.True;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }

        public async Task<Response<List<MenuDTO>>> GetListMenuByUserId(string userId)
        {
            var response = new Response<List<MenuDTO>>();
            try
            {
                if (userId == "099d3577-f643-4818-a965-91ba5a96de04")
                {
                    response.value = _mapper.Map<List<MenuDTO>>(await _MenuRepository.GetListAsync());
                    if (response.value.Count() > 0)
                        response.isSuccess = Constants.Status.True;
                }
                else
                {
                    IQueryable<User> tbUser = await _UserRepository.AsQueryable(x => x.UserId == new Guid(userId));
                    IQueryable<MenuPosition> tbMenuPosition = await _MenuPositionRepository.AsQueryable();
                    IQueryable<Menu> tbMenu = await _MenuRepository.AsQueryable();

                    IQueryable<Menu> tbResult = (from u in tbUser
                                                 join mp in tbMenuPosition on u.PositionId equals mp.PositionId
                                                 join m in tbMenu on mp.MenuId equals m.MenuId
                                                 select m).AsQueryable();
                    if (tbResult.Count() > 0)
                    {
                        response.value = _mapper.Map<List<MenuDTO>>(tbResult.ToList());
                        response.isSuccess = Constants.Status.True;
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }

        public Task<Response<List<MenuDTO>>> GetListMenuExists(string positionId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<MenuDTO>>> GetMenuByPositionId(string positionId)
        {
            throw new NotImplementedException();
        }
    }
}
