﻿using AutoMapper;
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
        private readonly IStoredRespository _storedRespository;
        private readonly IMapper _mapper;

        public MenuService(
            IGenericRepository<Menu> MenuRepository,
            IGenericRepository<User> UserRepository,
            IStoredRespository storedRespository,
            IMapper mapper)
        {
            _MenuRepository = MenuRepository;
            _UserRepository = UserRepository;
            _storedRespository = storedRespository;
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

        public async Task<Response<List<MenuDTO>>> GetMenuByPositionId(string positionId)
        {
            var response = new Response<List<MenuDTO>>();
            try
            {
                if (positionId == "P00")
                    response.value = _mapper.Map<List<MenuDTO>>(await _MenuRepository.GetListAsync(x => x.Status == Constants.Status.Active));
                else
                    response.value = _mapper.Map<List<MenuDTO>>(await _storedRespository.SP_GET_MENU_BY_POSITION(positionId));

                if (response.value.Count() > 0)
                    response.isSuccess = Constants.Status.True;
                else
                    response.message = Constants.StatusMessage.No_Data;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }
    }
}
