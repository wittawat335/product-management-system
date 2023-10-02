using Ecommerce.Web.Models;
using Ecommerce.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
            try
            {
                using (var client = new HttpClient(_httpClient))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.token);
                    HttpResponseMessage result = await client.GetAsync(path);
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

        public async Task<Response<T>> GetAsyncById(string path)
        {
            var session = _commonService.GetSessionValue();
            var response = new Response<T>();
            try
            {
                using (var client = new HttpClient(_httpClient))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.token);
                    HttpResponseMessage result = await client.GetAsync(path);
                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<T>>(data);
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }

        public async Task<Response<T>> InsertAsync(string path, T request)
        {
            var session = _commonService.GetSessionValue();
            var response = new Response<T>();
            try
            {
                using (var client = new HttpClient(_httpClient))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.token);
                    HttpResponseMessage result = await client.PostAsJsonAsync(path, request);

                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<T>>(data);
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
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
            try
            {
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
                        response.message = result.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }

        public Task<Response<T>> PostAsync(string path, T request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<T>> PutAsync(string path, T request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<T>> DeleteAsync(string path)
        {
            throw new NotImplementedException();
        }
    }
}
