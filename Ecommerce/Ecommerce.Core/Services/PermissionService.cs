using AutoMapper;
using Ecommerce.Core.Common;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Models;
using Ecommerce.Domain.RepositoryContracts;

namespace Ecommerce.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IGenericRepository<Permission> _repository;
        private readonly IGenericRepository<Position> _postRepository;
        private readonly IStoredRespository _storedRespository;
        private readonly IMapper _mapper;

        public PermissionService(IGenericRepository<Permission> repository, 
            IGenericRepository<Position> postRepository,
            IStoredRespository storedRespository, IMapper mapper)
        {
            _repository = repository;
            _postRepository = postRepository;
            _storedRespository = storedRespository;
            _mapper = mapper;
        }

        private string menuId;
        public async Task DeleteListByPositionId(string positionId)
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
            }
        }

        public async Task<Response<Permission>> Add(PermissionDTO model)
        {
            var response = new Response<Permission>();

            _repository.Insert(_mapper.Map<Permission>(model));
            response.isSuccess = Constants.Status.True;
            response.message = Constants.StatusMessage.InsertSuccessfully;

            return response;
        }

        public async Task<Response<List<PermissionDTO>>> GetList(string positionId)
        {
            var response = new Response<List<PermissionDTO>>();

            var query = await _repository.GetListAsync(x => x.PositionId == positionId);
            if (query.Count() > 0)
            {
                response.value = _mapper.Map<List<PermissionDTO>>(query);
                response.isSuccess = Constants.Status.True;
                response.message = Constants.StatusMessage.Success;
            }


            return response;
        }

        public async Task<Response<string>> SaveList(string id, 
            List<DataPermissionJsonInsertList> list)
        {
            var response = new Response<string>();

            if (list.Count() > 0)
            {
                await DeleteListByPositionId(id);
                await childrenList(id, list);
                await _repository.SaveChangesAsync();

                var query = await _repository.GetListAsync(x => x.PositionId == id);
                if (query.Count() == 0) response.message = "ยังไม่ได้กำหนดสิทธิ์";

                response.isSuccess = Constants.Status.True;
            }

            return response;
        }

        public async Task childrenList(string positionId, 
            List<DataPermissionJsonInsertList> model)
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

        public async Task<Response<List<DataPermissionJsonList>>> GetListJsTree(
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
    }
}
