namespace Ecommerce.Web.Models.Category
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
