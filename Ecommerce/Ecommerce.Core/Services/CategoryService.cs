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
        public Task<Response<CategoryDTO>> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Category>> Add(CategoryDTO model)
        {
            throw new NotImplementedException();
        }
        public Task<Response<Category>> Update(CategoryDTO model)
        {
            throw new NotImplementedException();
        }
        public Task<Response<Category>> Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
