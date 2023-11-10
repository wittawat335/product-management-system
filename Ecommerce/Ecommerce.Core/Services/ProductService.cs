using AutoMapper;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Ecommerce.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly ConnectionStringSettings _setting;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repository, IOptions<ConnectionStringSettings> options, IMapper mapper)
        {
            _setting = options.Value;
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
                    if (request.ProductId != null) result = result.Where(x => x.ProductId == request.ProductId).ToList();
                    if (request.ProductName != null) result = result.Where(x => x.ProductName.Contains(request.ProductName)).ToList();
                    if (request.CategoryId != null) result = result.Where(x => x.CategoryId == request.CategoryId).ToList();
                    if (request.CategoryName != null) result = result.Where(x => x.Category.CategoryName.Contains(request.CategoryName)).ToList();
                    if (request.Status != null) result = result.Where(x => x.Status.Contains(request.Status)).ToList();
                }
                if (result.Count() > 0)
                {
                    response.value = _mapper.Map<List<ProductDTO>>(result);
                    response.isSuccess = Constants.Status.True;
                }
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
                var query = await _repository.GetAsync(x => x.ProductId == id);
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
                var result = await CheckDupilcate(model.ProductId, model.ProductName, model.CategoryId);
                if (result == string.Empty)
                {
                    response.value = await _repository.InsertAsyncAndSave(_mapper.Map<Product>(model));
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
        public async Task<Response<Product>> Update(ProductDTO model)
        {
            var response = new Response<Product>();
            try
            {
                var data = _repository.Get(x => x.ProductId == model.ProductId);
                if (data != null)
                {
                    var result = await CheckDupilcate(null, model.ProductName, model.CategoryId);
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
        public async Task<Response<Product>> Delete(string id)
        {
            var response = new Response<Product>();
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
        public async Task<string> CheckDupilcate(string id, string name, string categoryId)
        {
            string message = string.Empty;
            if (id != null)
            {
                var checkDup = await _repository.GetAsync(x => x.ProductId == id);
                if (checkDup != null) message = string.Format(id + " " + Constants.StatusMessage.DuplicateId);
            }
            if (name != null)
            {
                var checkDup = await _repository.GetAsync(x => x.ProductName == name && x.CategoryId == categoryId);
                if (checkDup != null) message = string.Format(name + " " + Constants.StatusMessage.DuplicateName);
            }

            return message;
        }
    }
}
