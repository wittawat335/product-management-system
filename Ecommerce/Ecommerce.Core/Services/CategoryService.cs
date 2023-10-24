using AutoMapper;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.RepositoryContracts;

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
                var list = await _repository.AsQueryable();
                if (filter != null)
                {
                    if (filter.CategoryName != null && filter.CategoryName != "string")
                        list = list.Where(x => x.CategoryName.Contains(filter.CategoryName));
                    if (filter.Status != null && filter.Status != "string")
                        list = list.Where(x => x.Status.Contains(filter.Status));
                }
                response.value = _mapper.Map<List<CategoryDTO>>(list);
                response.isSuccess = Constants.Status.True;
            }
            catch (Exception ex)
            {
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
                response.value = await _repository.InsertAsyncAndSave(_mapper.Map<Category>(model));
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
        public async Task<Response<Category>> Update(CategoryDTO model)
        {
            var response = new Response<Category>();
            try
            {
                var data = _repository.Get(x => x.CategoryId == model.CategoryId);
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
    }
}
