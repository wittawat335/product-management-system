using AutoMapper;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<List<ProductDTO>>> GetList(ProductDTO request = null)
        {
            var response = new Response<List<ProductDTO>>();
            try
            {
                var productList = await _repository.AsQueryable();
                var result = productList.Include(x => x.Category).ToList();
                if (request != null)
                {
                    if (request.ProductName != null && request.ProductName != "string")
                        result = result.Where(x => x.ProductName.Contains(request.ProductName)).ToList();
                    if (request.CategoryName != null && request.CategoryName != "string")
                        result = result.Where(x => x.Category.CategoryName.Contains(request.CategoryName)).ToList();
                    if (request.Status != null && request.Status != "string")
                        result = result.Where(x => x.Status.Contains(request.Status)).ToList();
                }
                response.value = _mapper.Map<List<ProductDTO>>(result);
                response.isSuccess = Constants.Status.True;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        public async Task<Response<ProductDTO>> Get(string id)
        {
            var response = new Response<ProductDTO>();
            try
            {
                var query = await _repository.GetAsync(x => x.ProductId == new Guid(id));
                if (query != null)
                {
                    response.value = _mapper.Map<ProductDTO>(query);
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
        public async Task<Response<Product>> Add(ProductDTO model)
        {
            var response = new Response<Product>();
            try
            {
                response.value = await _repository.InsertAsyncAndSave(_mapper.Map<Product>(model));
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
        public async Task<Response<Product>> Update(ProductDTO model)
        {
            var response = new Response<Product>();
            try
            {
                var data = _repository.Get(x => x.ProductId == new Guid(model.ProductId));
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
        public async Task<Response<Product>> Delete(string id)
        {
            var response = new Response<Product>();
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
