using AutoMapper;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs.Category;
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
                var query = await _repository.GetListAsync();
                if (query.Count() > 0)
                {
                    response.value = _mapper.Map<List<CategoryDTO>>(query);
                    response.isSuccess = Constants.Status.True;
                    response.message = Constants.StatusMessage.Success;
                }
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
                    response.message = Constants.StatusMessage.Success;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }
        public async Task<Response<Category>> Insert(CategoryDTO model)
        {
            var response = new Response<Category>();
            try
            {
                var validate = await CheckDupilcate(model.CategoryId);
                if (validate == string.Empty)
                {
                    _repository.Insert(_mapper.Map<Category>(model));
                    await _repository.SaveChangesAsync();
                    response.isSuccess = Constants.Status.True;
                    response.message = Constants.StatusMessage.SaveSuccessfully;
                }
                else
                {
                    response.message = validate;
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
                var query = _repository.Get(x => x.CategoryId == model.CategoryId);
                if (query != null)
                {
                    _repository.Update(_mapper.Map(model, query));
                    await _repository.SaveChangesAsync();
                    response.isSuccess = Constants.Status.True;
                    response.message = Constants.StatusMessage.SaveSuccessfully;
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
                var query = _repository.Find(id);
                if (query != null)
                {
                    _repository.Delete(query);
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
        public async Task<string> CheckDupilcate(string id)
        {
            var query = await _repository.GetAsync(x => x.CategoryId == id);
            string message = (query != null) ? string.Format(id + " " + Constants.StatusMessage.DuplicateId) : string.Empty;

            return message;
        }
    }
}
