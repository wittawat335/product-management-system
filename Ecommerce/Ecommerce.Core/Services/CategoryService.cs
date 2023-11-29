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
                var query = await _repository.GetListAsync();
                if (query.Count() > 0)
                {
                    response.value = _mapper.Map<List<CategoryDTO>>(query);
                    response.isSuccess = Constants.Status.True;
                }
                else
                {
                    response.message = Constants.StatusMessage.No_Data;
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
                }
                else
                {
                    response.message = Constants.StatusMessage.No_Data;
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
                var messageCheck = await CheckDupilcate(model.CategoryId);
                if (messageCheck == string.Empty)
                {
                    _repository.Insert(_mapper.Map<Category>(model));
                    await _repository.SaveChangesAsync();
                    response.isSuccess = Constants.Status.True;
                    response.message = Constants.StatusMessage.InsertSuccessfully;
                }
                else
                {
                    response.message = messageCheck;
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
                    response.message = Constants.StatusMessage.UpdateSuccessfully;
                }
                else
                {
                    response.message = Constants.StatusMessage.No_Data;
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
                else
                {
                    response.message = Constants.StatusMessage.No_Data;
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
