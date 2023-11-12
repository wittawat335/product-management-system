using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models;
using Ecommerce.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Ecommerce.Web.Commons;
using System.Net.Http.Json;
using Ecommerce.Web.Models.Authen;
using Ecommerce.Web.Models.Permission;

namespace Ecommerce.Web.Services
{
    public class AuthenService : IAuthenService
    {
        private readonly AppSetting _setting;
        private readonly IHttpContextAccessor _contextAccessor;
        HttpClientHandler _httpClient = new HttpClientHandler();

        public AuthenService(IOptions<AppSetting> options, IHttpContextAccessor contextAccessor)
        {
            _setting = options.Value;
            _contextAccessor = contextAccessor;
            _httpClient.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public string GetIp()
        {
            throw new NotImplementedException();
        }

        public async Task GetPermission(string positionId)
        {
            var response = new Response<List<Permission>>();
            var path = string.Format(_setting.BaseApiUrl + "Permission/GetList/{0}", positionId);
            try
            {
                using (var client = new HttpClient(_httpClient))
                {
                    client.BaseAddress = new Uri(path);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage result = await client.GetAsync(path);
                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<List<Permission>>>(data);
                        if (response.isSuccess)
                        {
                            SetPermissionToSession(response.value);
                        }
                    }
                    else
                        response.message = Constants.MessageError.CallAPI;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
        }

        public async Task<Response<Session>> Login(Login request)
        {
            var response = new Response<Session>();
            var path = _setting.BaseApiUrl + "Authen/Login";
            try
            {
                using (var client = new HttpClient(_httpClient, false))
                {
                    client.BaseAddress = new Uri(path);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage result = await client.PostAsJsonAsync<Login>(path, request);
                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<Session>>(data);
                        if (response.isSuccess)
                            SetSessionValue(response.value);
                    }
                    else
                        response.message = Constants.MessageError.CallAPI;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Register>> Register(Register request)
        {
            var response = new Response<Register>();
            var path = _setting.BaseApiUrl + "Authen/Register";
            try
            {
                using (var client = new HttpClient(_httpClient))
                {
                    client.BaseAddress = new Uri(path);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage result = await client.PostAsJsonAsync<Register>(path, request);
                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<Register>>(data);
                    }
                    else
                        response.message = Constants.MessageError.CallAPI;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }

        public void SetPermissionToSession(List<Permission> listPermission)
        {
            try
            {
                string sessionString = JsonConvert.SerializeObject(listPermission);
                if (sessionString != null)
                    _contextAccessor.HttpContext.Session.SetString(Constants.SessionKey.permission, sessionString);
            }
            catch
            {
                throw;
            }
        }

        public void SetSessionValue(Session session)
        {
            try
            {
                string sessionString = JsonConvert.SerializeObject(session);
                string tokenString = JsonConvert.SerializeObject(session.token);
                if (sessionString != null)
                    _contextAccessor.HttpContext.Session.SetString(Constants.SessionKey.sessionLogin, sessionString);
                if (tokenString != null)
                    _contextAccessor.HttpContext.Session.SetString(Constants.SessionKey.accessToken, tokenString);
            }
            catch
            {
                throw;
            }
        }
    }
}
