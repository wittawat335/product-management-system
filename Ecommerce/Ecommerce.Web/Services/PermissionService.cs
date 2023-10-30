using Ecommerce.Web.Commons;
using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models;
using Ecommerce.Web.Models.Authen;
using Ecommerce.Web.Models.Permission;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;

namespace Ecommerce.Web.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly ICommonService _common;
        private readonly AppSetting _setting;
        private readonly IBaseApiService<Permission> _service;
        private readonly IHttpContextAccessor _contextAccessor;
        HttpClientHandler _httpClient = new HttpClientHandler();

        public PermissionService(
            ICommonService common,
            IBaseApiService<Permission> service,
            IHttpContextAccessor contextAccessor,
            IOptions<AppSetting> options)
        {
            _common = common;
            _service = service;
            _httpClient.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _contextAccessor = contextAccessor;
            _setting = options.Value;
        }
        private string menuId;
        public async Task<Response<List<TreeViewNode>>> GetJsTree(string path)
        {
            var session = _common.GetSessionValue();
            var response = new Response<List<TreeViewNode>>();
            try
            {
                using (var client = new HttpClient(_httpClient))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.token);
                    HttpResponseMessage result = await client.GetAsync(path);
                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<List<TreeViewNode>>>(data);
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<Response<Permission>> SavePermission(string positionId)
        {
            var response = new Response<Permission>();
            try
            {
                var list = _common.GetListSessionValue();
                if (list.Count() > 0)
                {
                    await _service.DeleteAsync(_setting.BaseApiUrl + string.Format("Permission/DeleteList/{0}", positionId));
                    await Save(positionId, list);
                    _contextAccessor.HttpContext.Session.Remove("listSelectedPermission");
                    response.isSuccess = true;
                }
            }
            catch (Exception ex)
            {

            }

            return response;
        }

        public async Task Save(string positionId, List<DataPermissionJsonInsertList> model)
        {
            try
            {
                foreach (DataPermissionJsonInsertList list in model)
                {
                    if (list.children.Count() > 0)
                    {
                        menuId = list.id;
                        await Save(positionId, list.children);
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
                            await PostListAsJsonAsync(_setting.BaseApiUrl + "Permission/SavePermission", perm);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<Response<Permission>> PostListAsJsonAsync(string path, Permission request)
        {
            var response = new Response<Permission>();
            try
            {
                using (var client = new HttpClient(_httpClient, false))
                {
                    HttpResponseMessage result = await client.PostAsJsonAsync(path, request);

                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<Permission>>(data);
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }

        public Response<List<DataPermissionJsonInsertList>> SetPermissionToSession(List<DataPermissionJsonInsertList> permissionData)
        {
            var response = new Response<List<DataPermissionJsonInsertList>>();
            try
            {
                response.value = permissionData;
                if (permissionData.Count() > 0)
                {
                    string session = JsonConvert.SerializeObject(permissionData);
                    _contextAccessor.HttpContext.Session.SetString("listSelectedPermission", session);
                    response.isSuccess = true;
                }
            }
            catch (Exception ex) { response.message = ex.Message; }

            return response;
        }
    }
}
