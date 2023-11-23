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
        private readonly IGenericRepository<Position> _postRepository;
        private readonly IMapper _mapper;

        public PermissionService(IGenericRepository<Permission> repository, IGenericRepository<Position> postRepository, IMapper mapper)
        {
            _repository = repository;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        private string menuId;
        public async Task<Response<Permission>> DeleteListByPositionId(string positionId)
        {
            var response = new Response<Permission>();
            try
            {
                var query = await _repository.GetListAsync(x => x.PositionId == positionId);
                var queryPosition = await _postRepository.GetAsync(x => x.PositionId == positionId);

                if (queryPosition != null)
                {
                    queryPosition.MenuDefault = null;
                    await _postRepository.SaveChangesAsync();
                }
                if (query.Count() > 0)
                {
                    _repository.DeleteList(query);
                    await _repository.SaveChangesAsync();
                    response.isSuccess = Constants.Status.True;
                }
                else response.message = "ยังไม่มี";
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
                if(model != null) 
                {
                    _repository.Insert(_mapper.Map<Permission>(model));
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

        public async Task<Response<string>> SaveList(string id, List<DataPermissionJsonInsertList> list)
        {
            var response = new Response<string>();
            try
            {
                if (list.Count() > 0)
                {
                    await DeleteListByPositionId(id);
                    await childrenList(id, list);
                    await _repository.SaveChangesAsync();

                    response.isSuccess = Constants.Status.True;
                    response.message = Constants.StatusMessage.SaveSuccessfully;
                }
            }
            catch (Exception ex) { response.message = ex.Message; }

            return response;
        }

        public async Task childrenList(string positionId, List<DataPermissionJsonInsertList> model)
        {
            try
            {
                foreach (DataPermissionJsonInsertList list in model)
                {
                    if (list.children.Count() > 0)
                    {
                        menuId = list.id;
                        await childrenList(positionId, list.children);
                    }
                    else
                    {
                        if (list.state.selected)
                        {
                            Permission perm = new Permission();
                            perm.PositionId = positionId;
                            perm.MenuId = menuId;
                            perm.ActId = list.id;
                            perm.Flag = "1";
                            perm.Status = "A";
                            _repository.Insert(perm);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
