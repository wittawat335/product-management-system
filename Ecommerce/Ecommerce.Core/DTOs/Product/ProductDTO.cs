namespace Ecommerce.Core.DTOs.Product
{
    public class ProductDTO
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public string Stock { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreateDate { get; set; }
    }
}
