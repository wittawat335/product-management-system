namespace Ecommerce.Web.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public ProductViewModel()
        {
            Product = new Product();
        }
    }
}
