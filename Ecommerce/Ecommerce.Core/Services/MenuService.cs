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
        private readonly IGenericRepository<Menu> _repository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Menu> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<MenuDTO>>> GetAllMenu()
        {
            var response = new Response<List<MenuDTO>>();
            try
            {
                response.value = _mapper.Map<List<MenuDTO>>(await _repository.GetListAsync());
                if (response.value.Count > 0)
                {
                    response.isSuccess = Constants.Status.True;
                    response.message = "";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }

        public Response<List<MenuDTO>> GetListMenuExists(string positionId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<MenuDTO>> GetMenuByPositionId(string positionId)
        {
            throw new NotImplementedException();
        }

        Task<Response<List<MenuDTO>>> IMenuService.GetMenuByPositionId(string positionId)
        {
            throw new NotImplementedException();
        }
    }
}
