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

        public PositionService(
            IGenericRepository<Position> repository, IStoredRespository storedRespository, IMapper mapper)
        {
            _repository = repository;
            _storedRespository = storedRespository;
            _mapper = mapper;
        }

        public async Task<Response<List<PositionDTO>>> GetList(PositionDTO filter = null)
        {
            var response = new Response<List<PositionDTO>>();
            try
            {
                var list = await _repository.AsQueryable();
                list = list.Include(x => x.MenuDefaultNavigation);
                if (filter != null)
                {
                    if (filter.Status != null) list = list.Where(x => x.Status.Contains(filter.Status));
                }
                if (list.Count() > 0)
                {
                    response.value = _mapper.Map<List<PositionDTO>>(list);
                    response.isSuccess = Constants.Status.True;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<Response<PositionDTO>> Get(string id)
        {
            var response = new Response<PositionDTO>();
            try
            {
                var query = await _repository.GetAsync(x => x.PositionId == id);
                if (query != null)
                {
                    response.value = _mapper.Map<PositionDTO>(query);
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

        public async Task<Response<Position>> Add(PositionDTO model)
        {
            var response = new Response<Position>();
            try
            {
                var position = await _repository.GetAsync(x => x.PositionName == model.PositionName);
                if (position != null) response.message = Constants.StatusMessage.DuplicatePosition;
                else
                {
                    response.value = await _repository.InsertAsyncAndSave(_mapper.Map<Position>(model));
                    if (response.value != null)
                    {
                        response.isSuccess = Constants.Status.True;
                        response.message = Constants.StatusMessage.AddSuccessfully;
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<Response<Position>> Update(PositionDTO model)
        {
            var response = new Response<Position>();
            try
            {
                var data = _repository.Get(x => x.PositionId == model.PositionId);
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
                response.message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }

        public async Task<Response<Position>> Delete(string id)
        {
            var response = new Response<Position>();
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

        public async Task<Response<List<DataPermissionJsonList>>> GetListPermissionData(string positionId)
        {
            var response = new Response<List<DataPermissionJsonList>>();
            List<DataPermissionJsonList> objReturn = new List<DataPermissionJsonList>();
            try
            {
                List<SP_GET_PERMISSION_BY_POSITION_RESULT> objData = await _storedRespository.SP_GET_PERMISSION_BY_POSITION(positionId);
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

                        string strParent = (string.IsNullOrEmpty(objData[i].PERM_PARENT)) ? "#" : objData[i].PERM_PARENT;
                        bool booParentOpen = false;
                        OptionState objStates = new OptionState { opened = booParentOpen, selected = varSelect };
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
                    response.value = objReturn;

                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
