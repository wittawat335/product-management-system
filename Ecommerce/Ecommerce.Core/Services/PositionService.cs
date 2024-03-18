using AutoMapper;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Models;
using Ecommerce.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Services
{
    public class PositionService : IPositionService
    {
        private readonly IGenericRepository<Position> _repository;
        private readonly IStoredRespository _storedRespository;
        private readonly IMapper _mapper;

        public PositionService(IGenericRepository<Position> repository,
            IStoredRespository storedRespository,
            IMapper mapper)
        {
            _repository = repository;
            _storedRespository = storedRespository;
            _mapper = mapper;
        }

        public async Task<Response<List<PositionDTO>>> GetList(PositionDTO request = null)
        {
            var response = new Response<List<PositionDTO>>();
            var query = await _repository.AsQueryable();
            query = query.Include(x => x.MenuDefaultNavigation);
            if (request != null)
            {
                query = (request.Status != null)
                    ? query.Where(x => x.Status.Contains(request.Status)) : query;
            }
            if (query.Count() > 0)
            {
                response.value = _mapper.Map<List<PositionDTO>>(query);
                response.isSuccess = Constants.Status.True;
                response.message = Constants.StatusMessage.Success;
            }

            return response;
        }
        public async Task<Response<PositionDTO>> Get(string id)
        {
            var response = new Response<PositionDTO>();
            var query = await _repository.GetAsync(x => x.PositionId == id);
            if (query != null)
            {
                response.value = _mapper.Map<PositionDTO>(query);
                response.isSuccess = Constants.Status.True;
                response.message = Constants.StatusMessage.Success;
            }

            return response;
        }
        public async Task<Response<Position>> Insert(PositionDTO model)
        {
            var response = new Response<Position>();
            var validate = await CheckDupilcate(model.PositionId);
            if (validate == string.Empty)
            {
                _repository.Insert(_mapper.Map<Position>(model));
                await _repository.SaveChangesAsync();
                response.isSuccess = Constants.Status.True;
                response.message = Constants.StatusMessage.SaveSuccessfully;
            }
            else
            {
                response.message = validate;
            }

            return response;
        }
        public async Task<Response<Position>> Update(PositionDTO model)
        {
            var response = new Response<Position>();
            var query = _repository.Get(x => x.PositionId == model.PositionId);
            if (query != null)
            {
                _repository.Update(_mapper.Map(model, query));
                await _repository.SaveChangesAsync();
                response.isSuccess = Constants.Status.True;
                response.message = Constants.StatusMessage.SaveSuccessfully;
            }

            return response;
        }
        public async Task<Response<Position>> Delete(string id)
        {
            var response = new Response<Position>();
            var query = _repository.Find(id);
            if (query != null)
            {
                _repository.Delete(query);
                await _repository.SaveChangesAsync();
                response.isSuccess = Constants.Status.True;
                response.message = Constants.StatusMessage.DeleteSuccessfully;
            }

            return response;
        }
        public async Task<Response<List<DataPermissionJsonList>>> GetListPermissionData(
            string positionId)
        {
            var response = new Response<List<DataPermissionJsonList>>();
            List<DataPermissionJsonList> objReturn = new List<DataPermissionJsonList>();
            List<SP_GET_PERMISSION_BY_POSITION_RESULT> objData = 
                await _storedRespository.SP_GET_PERMISSION_BY_POSITION(positionId);
            if (objData != null && objData.Count > 0)
            {
                int? iCountTopLevel = objData.Count;
                for (int i = 0; i < iCountTopLevel; i++)
                {
                    bool varSelect = false;
                    if (objData[i].PERM_SELECT == "1")
                        varSelect = true;

                    string strIcon;
                    switch (objData[i].PERM_TEXT)
                    {
                        case Constants.JsTreeConfig.TextAdd:
                            strIcon = Constants.JsTreeConfig.IconAdd;
                            break;
                        case Constants.JsTreeConfig.TextEdit:
                            strIcon = Constants.JsTreeConfig.IconEdit;
                            break;
                        case Constants.JsTreeConfig.TextView:
                            strIcon = Constants.JsTreeConfig.IconView;
                            break;
                        default:
                            {
                                strIcon = Constants.JsTreeConfig.IconDefault;
                                break;
                            }
                    }

                    string strParent = (string.IsNullOrEmpty(objData[i].PERM_PARENT)) 
                        ? "#" : objData[i].PERM_PARENT;
                    bool booParentOpen = false;
                    OptionState objStates = new OptionState 
                    { 
                        opened = booParentOpen, 
                        selected = varSelect 
                    };
                    objReturn.Add(new DataPermissionJsonList()
                    {
                        id = objData[i].PERM_ID.ToString(),
                        parent = strParent.ToString(),
                        text = objData[i].PERM_TEXT,
                        icon = strIcon,
                        state = objStates
                    });
                }
            }

            if (objReturn.Count() > 0)
            {
                response.value = objReturn;
                response.isSuccess = Constants.Status.True;
            }
            return response;
        }
        public async Task<string> CheckDupilcate(string id)
        {

            var checkDup = await _repository.GetAsync(x => x.PositionId == id);
            string message = checkDup != null 
                ? string.Format(id + " " + Constants.StatusMessage.DuplicateId) : string.Empty;

            return message;
        }
    }
}
