using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Ecommerce.Web.Commons;

namespace Ecommerce.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseApiService<Product> _productService;
        private readonly ICommonService _common;
        private readonly AppSetting _setting;
        HttpClientHandler _httpClient = new HttpClientHandler();

        public ProductService(IBaseApiService<Product> productService, ICommonService common, IOptions<AppSetting> options)
        {
            _productService = productService;
            _common = common;
            _setting = options.Value;
            _httpClient.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public Task<Product> Detail(string id, string action)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Product>>> GetList(string url, Product filter)
        {
            var session = _common.GetSessionValue();
            var response = new Response<List<Product>>();
            try
            {
                using (var client = new HttpClient(_httpClient))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.token);
                    HttpResponseMessage result = await client.PostAsJsonAsync(url, filter);
                    if (result.IsSuccessStatusCode)
                    {
                        string data = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response<List<Product>>>(data);
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

        public Task<Response<Product>> Save(Product model)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> Select2Product(string url, string query)
        {
            throw new NotImplementedException();
        }
    }
}
