using AutoMapper;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;
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

        public async Task<Response<List<ProductDTO>>> GetList(ProductDTO filter = null)
        {
            var response = new Response<List<ProductDTO>>();
            try
            {
                var productList = await _repository.AsQueryable();
                var result = productList.Include(x => x.Category).ToList();
                if (filter != null)
                {
                    if (filter.ProductName != null && filter.ProductName != "string")
                        result = result.Where(x => x.ProductName.Contains(filter.ProductName)).ToList();
                    if (filter.CategoryName != null && filter.CategoryName != "string")
                        result = result.Where(x => x.Category.CategoryName.Contains(filter.CategoryName)).ToList();
                    if (filter.Status != null && filter.Status != "string")
                        result = result.Where(x => x.Status.Contains(filter.Status)).ToList();
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
    }
}
