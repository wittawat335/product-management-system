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
                var product = await _repository.GetAsync(x => x.ProductId == id);
                if (product != null)
                {
                    response.value = _mapper.Map<ProductDTO>(product);
                    response.isSuccess = Constants.Status.True;
                    response.message = Constants.StatusMessage.InsertSuccessfully;
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
                var checkMsg = await CheckDupilcate(model.ProductId, model.ProductName, model.CategoryId);
                if (checkMsg == string.Empty)
                {
                    response.value = await _repository.InsertAsyncAndSave(_mapper.Map<Product>(model));
                    if (response.value != null)
                    {
                        response.isSuccess = Constants.Status.True;
                        response.message = Constants.StatusMessage.InsertSuccessfully;
                    }
                }
                else response.message = checkMsg;
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
                var product = _repository.Get(x => x.ProductId == model.ProductId);
                if (product != null)
                {
                    response.value = await _repository.UpdateAndSaveAsync(_mapper.Map(model, product));
                    if (response.value != null)
                    {
                        response.isSuccess = Constants.Status.True;
                        response.message = Constants.StatusMessage.UpdateSuccessfully;
                    }
                }
                else response.message = Constants.StatusMessage.No_Data;
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
                var product = _repository.Find(id);
                if (product != null)
                {
                    _repository.Delete(product);
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
                message = (checkDup != null) ? string.Format(id + " " + Constants.StatusMessage.DuplicateId) : string.Empty;
            }

            return message;
        }
        public async Task<Response<Product>> Upload(List<ProductDTO> request)
        {
            var response = new Response<Product>();
            try
            {
                if (request.Count() > 0)
                {
                    foreach (var item in request)
                    {
                        var result = await CheckDupilcate(item.ProductId, item.ProductName, item.CategoryId);
                        if (result == string.Empty)
                            _repository.Insert(_mapper.Map<Product>(item));
                    }
                }

                await _repository.SaveChangesAsync();
                response.isSuccess = Constants.Status.True;
                response.message = Constants.StatusMessage.InsertSuccessfully;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
    }
}
