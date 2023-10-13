using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.Extensions.Options;
using Ecommerce.Web.Commons;
namespace Ecommerce.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseApiService<Product> _productService;
        private readonly IBaseApiService<Category> _categoryService;
        private readonly ICommonService _common;
        private readonly AppSetting _setting;
        HttpClientHandler _httpClient = new HttpClientHandler();

        public ProductService(
            IBaseApiService<Product> productService,
            IBaseApiService<Category> categoryService,
            ICommonService common,
            IOptions<AppSetting> options)
        {
            _productService = productService;
            _categoryService = categoryService;
            _common = common;
            _setting = options.Value;
            _httpClient.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public Task<Product> Detail(string id, string action)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Product>>> GetListProduct(Product filter)
        {
            var response = new Response<List<Product>>();
            response = await _productService.Search(_setting.BaseApiUrl + "Product/GetList", filter);

            return response;
        }

        public async Task<Response<List<Category>>> GetListCategory()
        {
            var response = await _categoryService.GetListAsync(_setting.BaseApiUrl + "Category/GetList");

            return response;
        }

        public async Task<Response<Product>> Save(ProductViewModel model)
        {
            var response = new Response<Product>();
            try
            {
                if (model != null)
                {
                    switch (model.Action)
                    {
                        case Constants.Action.Add:
                            response = await _productService.InsertAsync(_setting.BaseApiUrl + "Product/Add", model.Product);
                            break;
                        case Constants.Action.Update:
                            response = await _productService.PutAsync(_setting.BaseApiUrl + "Product/Update", model.Product);
                            break;
                        default:
                            response.message = Constants.MessageError.CallAPI;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }

        public async Task<List<Product>> Select2Product(string query)
        {
            var filter = new Product();
            var response = await GetListProduct(filter);
            response.value = response.value.Where(x => x.ProductName.ToLower().Contains(query.ToLower())).ToList();

            return response.value;
        }

        public async Task<List<Category>> Select2Category(string query)
        {
            var response = await GetListCategory();
            response.value = response.value.Where(x => x.CategoryName.ToLower().Contains(query.ToLower())).ToList();

            return response.value;
        }

        public async Task<Response<List<Product>>> GetListShopping(int pageSize, int p)
        {
            var filter = new Product();
            var response = await GetListProduct(filter);
            response.value = response.value.OrderByDescending(p => p.ProductName).Skip((p - 1) * pageSize).Take(pageSize).ToList();

            return response;
        }

        //public async Task<Response<List<Product>>> Search(string url, Product filter)
        //{
        //    var session = _common.GetSessionValue();
        //    var response = new Response<List<Product>>();
        //    try
        //    {
        //        using (var client = new HttpClient(_httpClient))
        //        {
        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.token);
        //            HttpResponseMessage result = await client.PostAsJsonAsync(url, filter);
        //            if (result.IsSuccessStatusCode)
        //            {
        //                string data = result.Content.ReadAsStringAsync().Result;
        //                response = JsonConvert.DeserializeObject<Response<List<Product>>>(data);
        //            }
        //            else
        //                response.message = Constants.MessageError.CallAPI;
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return response;
        //}
    }
}
