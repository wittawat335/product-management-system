namespace Ecommerce.Web.Models
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public string Action { get; set; }
        public CategoryViewModel()
        {
            Category = new Category();
        }
    }
}
