using AutoMapper;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs.Product;
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
                var query = await _repository.AsQueryable();
                query = query.Include(x => x.Category);
                query = query.Where(x => x.Category.Status == Constants.Status.Active);
                if (request != null)
                {
                    query = (request.ProductId != null) ? query.Where(x => x.ProductId == request.ProductId) : query;
                    query = (request.CategoryId != null) ? query.Where(x => x.CategoryId == request.CategoryId) : query;
                    query = (request.Status != null) ? query.Where(x => x.Status.Contains(request.Status)) : query;
                }
                if (query.Count() > 0)
                {
                    response.value = _mapper.Map<List<ProductDTO>>(query);
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
                    response.message = Constants.StatusMessage.Success;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }
        public async Task<Response<Product>> Insert(ProductDTO model)
        {
            var response = new Response<Product>();
            try
            {
                var validate = await CheckDupilcate(model.ProductId);
                if (validate == string.Empty)
                {
                    var returnValue = await _repository.InsertAsyncAndSave(_mapper.Map<Product>(model));
                    if(returnValue != null)
                    {
                        response.value = returnValue;
                        response.isSuccess = Constants.Status.True;
                        response.message = Constants.StatusMessage.SaveSuccessfully;
                    }
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
        public async Task<Response<Product>> Update(ProductDTO model)
        {
            var response = new Response<Product>();
            try
            {
                var query = _repository.Get(x => x.ProductId == model.ProductId);
                if (query != null)
                {
                    var returnValue = await _repository.UpdateAndSaveAsync(_mapper.Map(model, query));
                    if (returnValue != null)
                    {
                        response.value = returnValue;
                        response.isSuccess = Constants.Status.True;
                        response.message = Constants.StatusMessage.SaveSuccessfully;
                    }
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

            var query = await _repository.GetAsync(x => x.ProductId == id);
            string message = (query != null) ? string.Format(id + " " + Constants.StatusMessage.DuplicateId) : string.Empty;

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
                        var result = await CheckDupilcate(item.ProductId);
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
