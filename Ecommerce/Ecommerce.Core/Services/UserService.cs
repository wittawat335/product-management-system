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
        private readonly IGenericRepository<Position> _positionRepository;
        private readonly IGenericRepository<UserPosition> _upRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> repository, IGenericRepository<Position> positionRepository,
            IGenericRepository<UserPosition> upRepository, IMapper mapper)
        {
            _repository = repository;
            _positionRepository = positionRepository;
            _upRepository = upRepository;
            _mapper = mapper;
        }

        public async Task<Response<User>> Add(UserDTO model)
        {
            var response = new Response<User>();
            try
            {
                response.value = await _repository.InsertAsyncAndSave(_mapper.Map<User>(model));
                if (response.value != null)
                {
                    var result = await _upRepository.InsertAsyncAndSave(_mapper.Map<UserPosition>(response.value)); // Insert Table UserPosition
                    if (result != null)
                    {
                        response.isSuccess = Constants.Status.True;
                        response.message = Constants.StatusMessage.AddSuccessfully;
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<Response<User>> Delete(string id)
        {
            var response = new Response<User>();
            try
            {
                var data = _repository.Find(new Guid(id));
                if (data != null)
                {
                    _repository.Delete(data);
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

        public async Task<Response<UserDTO>> Get(string id)
        {
            var response = new Response<UserDTO>();
            try
            {
                var query = await _repository.GetAsync(x => x.UserId == new Guid(id));
                if (query != null)
                {
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

                response.value = _mapper.Map<List<UserDTO>>(result);
                response.isSuccess = Constants.Status.True;
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
                    response.value = await _repository.UpdateAndSaveAsync(_mapper.Map(model, data));
                    if (response.value != null)
                    {
                        var findPosition = await _upRepository.GetListAsync(x => x.UserId == new Guid(model.UserId));
                        if (findPosition.Count() > 0)
                        {
                            _upRepository.DeleteList(findPosition.ToList());
                            var result = await _upRepository.InsertAsyncAndSave(_mapper.Map<UserPosition>(model)); // Insert Table UserPosition
                            if (result != null)
                            {
                                response.isSuccess = Constants.Status.True;
                                response.message = Constants.StatusMessage.UpdateSuccessfully;
                            }
                        }
                        else
                        {
                            response.isSuccess = Constants.Status.True;
                            response.message = Constants.StatusMessage.UpdateSuccessfully;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
    }
}
