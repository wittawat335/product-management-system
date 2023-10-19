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
        private readonly IMapper _mapper;

        public MenuService(
            IGenericRepository<Menu> MenuRepository,
            IGenericRepository<User> UserRepository,
            IMapper mapper)
        {
            _MenuRepository = MenuRepository;
            _UserRepository = UserRepository;
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
