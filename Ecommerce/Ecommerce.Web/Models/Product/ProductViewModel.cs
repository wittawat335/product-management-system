using Ecommerce.Web.Models.Category;
namespace Ecommerce.Web.Models.Product
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public string Action { get; set; }
        public ProductViewModel()
        {
            Product = new Product();
        }
    }
}
