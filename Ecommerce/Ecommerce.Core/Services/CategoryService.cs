using AutoMapper;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.RepositoryContracts;
using System.Net;

namespace Ecommerce.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDTO>>> GetList(CategoryDTO filter = null)
        {
            var response = new Response<List<CategoryDTO>>();
            try
            {
                var list = await _repository.GetListAsync();
                if (list.Count() > 0)
                {
                    response.value = _mapper.Map<List<CategoryDTO>>(list);
                    response.isSuccess = Constants.Status.True;
                }
            }
            catch (Exception ex)
            {
                response.statusCode = (int)HttpStatusCode.InternalServerError;
                response.message = ex.Message;
            }
            return response;
        }
        public async Task<Response<CategoryDTO>> Get(string id)
        {
            var response = new Response<CategoryDTO>();
            try
            {
                var query = await _repository.GetAsync(x => x.CategoryId == id);
                if (query != null)
                {
                    response.value = _mapper.Map<CategoryDTO>(query);
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

        public async Task<Response<Category>> Add(CategoryDTO model)
        {
            var response = new Response<Category>();
            try
            {
                var result = await CheckDupilcate(model);
                if (result == string.Empty)
                {
                    response.value = await _repository.InsertAsyncAndSave(_mapper.Map<Category>(model));
                    if (response.value != null)
                    {
                        response.isSuccess = Constants.Status.True;
                        response.message = Constants.StatusMessage.AddSuccessfully;
                    }
                }
                else response.message = result;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public async Task<Response<Category>> Update(CategoryDTO model)
        {
            var response = new Response<Category>();
            try
            {
                var data = _repository.Get(x => x.CategoryId == model.CategoryId);
                if (data != null)
                {
                    var result = await CheckDupilcate(model);
                    if (result == string.Empty)
                    {
                        response.value = await _repository.UpdateAndSaveAsync(_mapper.Map(model, data));
                        if (response.value != null)
                        {
                            response.isSuccess = Constants.Status.True;
                            response.message = Constants.StatusMessage.UpdateSuccessfully;
                        }
                    }
                    else response.message = result;
                }
            }
            catch (Exception ex)
            {
                response.message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
        public async Task<Response<Category>> Delete(string id)
        {
            var response = new Response<Category>();
            try
            {
                var data = _repository.Find(id);
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
        public async Task<string> CheckDupilcate(CategoryDTO model)
        {
            string message = string.Empty;
            if (model.CategoryId != null)
            {
                var checkDup = await _repository.GetAsync(x => x.CategoryId == model.CategoryId);
                if (checkDup != null) message = Constants.StatusMessage.DuplicateId;
            }
            if (model.CategoryName != null)
            {
                var checkDup = await _repository.GetAsync(x => x.CategoryName == model.CategoryName);
                if (checkDup != null) message = Constants.StatusMessage.DuplicateName;
            }

            return message;
        }
    }
}
