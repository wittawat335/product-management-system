using Ecommerce.Web.Commons;
using Ecommerce.Web.Models;
using Ecommerce.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace Ecommerce.Web.Services
{
    public class BaseApiService<T> : IBaseApiService<T> where T : class
    {
        private readonly ICommonService _commonService;
        HttpClientHandler _httpClient = new HttpClientHandler();
        public BaseApiService(ICommonService commonService)
        {
            _httpClient.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _commonService = commonService;
        }

        public async Task<Response<List<T>>> GetListAsync(string path)
        {
            var session = _commonService.GetSessionValue();
            var response = new Response<List<T>>();
            using (var client = new HttpClient(_httpClient))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.token);
                HttpResponseMessage result = await client.GetAsync(path);
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response<List<T>>>(data);
                }
                else
                    response.returnUrl = String.Format("~/ErrorPages/{0}", (int)result.StatusCode);
            }

            return response;
        }
        public async Task<Response<T>> GetAsyncById(string path)
        {
            var session = _commonService.GetSessionValue();
            var response = new Response<T>();
            using (var client = new HttpClient(_httpClient))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.token);
                HttpResponseMessage result = await client.GetAsync(path);
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response<T>>(data);
                }
                else
                    response.returnUrl = String.Format("~/ErrorPages/{0}", (int)result.StatusCode);
            }

            return response;
        }
        public async Task<Response<T>> InsertAsync(string path, T request)
        {
            var session = _commonService.GetSessionValue();
            var response = new Response<T>();
            using (var client = new HttpClient(_httpClient))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.token);
                HttpResponseMessage result = await client.PostAsJsonAsync(path, request);
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response<T>>(data);
                }
                else
                    response.returnUrl = String.Format("~/ErrorPages/{0}", (int)result.StatusCode);
            }

            return response;
        }
        public Task<Response<T>> PatchAsync(string path, T request)
        {
            throw new NotImplementedException();
        }
        public async Task<Response<T>> PostAsJsonAsync(string path, T request)
        {
            var response = new Response<T>();
            var session = _commonService.GetSessionValue();
            using (var client = new HttpClient(_httpClient))
            {
                client.BaseAddress = new Uri(path);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.token);

                HttpResponseMessage result = await client.PostAsJsonAsync<T>(path, request);
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response<T>>(data);
                }
                else
                    response.returnUrl = String.Format("~/ErrorPages/{0}", (int)result.StatusCode);
            }

            return response;
        }
        public Task<Response<T>> PostAsync(string path, T request)
        {
            throw new NotImplementedException();
        }
        public async Task<Response<T>> PutAsync(string path, T request)
        {
            var session = _commonService.GetSessionValue();
            var response = new Response<T>();
            using (var client = new HttpClient(_httpClient))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.token);
                HttpResponseMessage result = await client.PutAsJsonAsync(path, request);

                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response<T>>(data);
                }
                else
                    response.returnUrl = String.Format("~/ErrorPages/{0}", (int)result.StatusCode);
            }

            return response;
        }
        public async Task<Response<T>> DeleteAsync(string path)
        {
            var session = _commonService.GetSessionValue();
            var response = new Response<T>();
            using (var client = new HttpClient(_httpClient))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.token);
                HttpResponseMessage result = await client.DeleteAsync(path);
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response<T>>(data);
                }
                else
                    response.returnUrl = String.Format("~/ErrorPages/{0}", (int)result.StatusCode);
            }

            return response;
        }
        public async Task<Response<List<T>>> Search(string path, T request)
        {
            var session = _commonService.GetSessionValue();
            var response = new Response<List<T>>();
            using (var client = new HttpClient(_httpClient))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.token);
                HttpResponseMessage result = await client.PostAsJsonAsync(path, request);
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response<List<T>>>(data);
                }
                else
                {
                    response.returnUrl = String.Format("~/ErrorPages/{0}", (int)result.StatusCode);
                }
            }

            return response;
        }

        public async Task<Response<List<T>>> PostListAsJsonAsync(string path, List<T> request)
        {
            var session = _commonService.GetSessionValue();
            var response = new Response<List<T>>();
            try
            {
                using (var client = new HttpClient(_httpClient))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.token);
                    HttpResponseMessage result = await client.PostAsJsonAsync(path, request);

                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<List<T>>>(data);
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }
    }
}
