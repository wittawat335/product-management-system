namespace Ecommerce.Web.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<Category> listCategory { get; set; }
        public string Action { get; set; }
        public ProductViewModel()
        {
            Product = new Product();
            listCategory = new List<Category>();
        }
    }
}
