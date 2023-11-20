namespace Ecommerce.Core.DTOs
{
    public class FileUploadDTO
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public string Stock { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
    }
}
