using AutoMapper;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.RepositoryContracts;

namespace Ecommerce.Core.Services
{
    public class PositionService : IPositionService
    {
        private readonly IGenericRepository<Position> _repository;
        private readonly IMapper _mapper;

        public PositionService(IGenericRepository<Position> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<PositionDTO>>> GetList(PositionDTO filter = null)
        {
            var response = new Response<List<PositionDTO>>();
            try
            {
                var list = await _repository.AsQueryable();
                if (filter != null)
                {
                    if (filter.Status != null) list = list.Where(x => x.Status.Contains(filter.Status));
                }
                if (list.Count() > 0)
                {
                    response.value = _mapper.Map<List<PositionDTO>>(list);
                    response.isSuccess = Constants.Status.True;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<Response<PositionDTO>> Get(string id)
        {
            var response = new Response<PositionDTO>();
            try
            {
                var query = await _repository.GetAsync(x => x.PositionId == new Guid(id));
                if (query != null)
                {
                    response.value = _mapper.Map<PositionDTO>(query);
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

        public async Task<Response<Position>> Add(PositionDTO model)
        {
            var response = new Response<Position>();
            try
            {
                response.value = await _repository.InsertAsyncAndSave(_mapper.Map<Position>(model));
                if (response.value != null)
                {
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

        public async Task<Response<Position>> Update(PositionDTO model)
        {
            var response = new Response<Position>();
            try
            {
                var data = _repository.Get(x => x.PositionId == new Guid(model.PositionId));
                if (data != null)
                {
                    response.value = await _repository.UpdateAndSaveAsync(_mapper.Map(model, data));
                    if (response.value != null)
                    {
                        response.isSuccess = Constants.Status.True;
                        response.message = Constants.StatusMessage.UpdateSuccessfully;
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

        public async Task<Response<Position>> Delete(string id)
        {
            var response = new Response<Position>();
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
    }
}
