using AutoMapper;
using Azure.Core;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly ICommonService _commonService;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> repository, ICommonService commonService, IMapper mapper)
        {
            _repository = repository;
            _commonService = commonService;
            _mapper = mapper;
        }
        public async Task<Response<UserDTO>> Get(string id)
        {
            var response = new Response<UserDTO>();
            try
            {
                var query = await _repository.GetAsync(x => x.UserId == new Guid(id));
                if (query != null)
                {
                    query.Password = _commonService.Decrypt(query.Password);
                    response.value = _mapper.Map<UserDTO>(query);
                    response.isSuccess = Constants.Status.True;
                    response.message = Constants.StatusMessage.AddSuccessfully;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }
        public async Task<Response<List<UserDTO>>> GetList(UserDTO filter = null)
        {
            var response = new Response<List<UserDTO>>();
            try
            {
                var list = await _repository.AsQueryable();
                var result = list.Include(x => x.Position).ToList();
                if (filter != null)
                {
                    if (filter.UserId != null) result = result.Where(x => x.UserId == new Guid(filter.UserId)).ToList();
                    if (filter.PositionId != null) result = result.Where(x => x.PositionId == filter.PositionId).ToList();
                    if (filter.Status != null) result = result.Where(x => x.Status.Contains(filter.Status)).ToList();
                }
                if (result != null)
                {
                    response.value = _mapper.Map<List<UserDTO>>(result);
                    response.isSuccess = Constants.Status.True;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public async Task<Response<User>> Add(UserDTO model)
        {
            var response = new Response<User>();
            try
            {
                var user = await _repository.GetAsync(x => x.Username == model.Username);
                if (user == null)
                {
                    model.UserId = null;
                    model.Password = _commonService.Encrypt(model.Password);
                    var mapping = _mapper.Map<User>(model);
                    var result = await _repository.InsertAsyncAndSave(mapping);
                    if (result != null)
                    {
                        response.isSuccess = Constants.Status.True;
                        response.message = Constants.StatusMessage.AddSuccessfully;
                    }
                }
                else response.message = Constants.StatusMessage.DuplicateUser;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public async Task<Response<User>> Update(UserDTO model)
        {
            var response = new Response<User>();
            try
            {
                var data = _repository.Get(x => x.UserId == new Guid(model.UserId));
                if (data != null)
                {
                    if (model.Password != null) model.Password = _commonService.Encrypt(model.Password);
                    _repository.Update(_mapper.Map(model, data));
                    await _repository.SaveChangesAsync();
                    response.isSuccess = Constants.Status.True;
                    response.message = Constants.StatusMessage.UpdateSuccessfully;
                }
            }
            catch (Exception ex)
            {
                response.message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
        public async Task<Response<User>> Delete(string id)
        {
            var response = new Response<User>();
            try
            {
                var userData = await _repository.GetAsync(x => x.UserId == new Guid(id));
                if (userData != null)
                {
                    _repository.Delete(userData);
                    await _repository.SaveChangesAsync();
                    response.isSuccess = Constants.Status.True;
                    response.message = Constants.StatusMessage.DeleteSuccessfully;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
    }
}
