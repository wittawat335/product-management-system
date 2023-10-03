using AutoMapper;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        public Response<List<ProductDTO>> GetList()
        {
            var response = new Response<List<ProductDTO>>();
            try
            {
                var productList = _repository.AsQueryable();
                var result = productList.Include(x => x.Category).ToList();
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
