using Ecommerce.Web.Models;
using Ecommerce.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Ecommerce.Web.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly ICommonService _commonService;
        HttpClientHandler _httpClient = new HttpClientHandler();

        public PermissionService(ICommonService commonService)
        {
            _commonService = commonService;
            _httpClient.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public async Task<Response<List<TreeViewNode>>> GetJsTree(string path)
        {
            var session = _commonService.GetSessionValue();
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
    }
}
