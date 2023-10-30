using AutoMapper;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.RepositoryContracts;
using System.Net;

namespace Ecommerce.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IGenericRepository<Permission> _repository;
        private readonly IMapper _mapper;

        public PermissionService(IGenericRepository<Permission> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<Permission>> DeleteListByPositionId(string positionId)
        {
            var response = new Response<Permission>();
            try
            {
                var query = await _repository.GetListAsync(x => x.PositionId == positionId);
                if (query.Count() > 0)
                {
                    _repository.DeleteList(query);
                    await _repository.SaveChangesAsync();
                    response.isSuccess = Constants.Status.True;
                }
                else
                {
                    response.message = "ยังไม่มี";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<Response<Permission>> Add(PermissionDTO model)
        {
            var response = new Response<Permission>();
            try
            {
                _repository.Insert(_mapper.Map<Permission>(model));
                response.isSuccess = Constants.Status.True;
                response.message = Constants.StatusMessage.AddSuccessfully;

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<Response<Permission>> SavePermission(PermissionDTO model)
        {
            var response = new Response<Permission>();
            try
            {
                _repository.Insert(_mapper.Map<Permission>(model));
                await _repository.SaveChangesAsync();
                response.isSuccess = Constants.Status.True;
                response.message = Constants.StatusMessage.AddSuccessfully;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        //public async Task<bool> IsPermission(string positionId)
        //{
        //    bool isPer = false;
        //    try
        //    {
        //        var query = await _repository
        //            .GetAsync(x => x.PositionId == positionId);
        //        if (query != null)
        //            isPer = true;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return isPer;
        //}

        public async Task<Response<List<PermissionDTO>>> GetList(string positionId)
        {
            var response = new Response<List<PermissionDTO>>();
            try
            {
                var list = await _repository.GetListAsync(x => x.PositionId == positionId);
                if (list.Count() > 0)
                {
                    response.value = _mapper.Map<List<PermissionDTO>>(list);
                    response.isSuccess = Constants.Status.True;
                }
            }
            catch (Exception ex)
            {
                response.statusCode = (int)HttpStatusCode.InternalServerError;
                response.message = ex.Message;
            }
            return response;
        }
    }
}
