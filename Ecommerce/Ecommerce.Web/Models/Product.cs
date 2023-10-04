namespace Ecommerce.Web.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreateDate { get; set; }
    }
}
