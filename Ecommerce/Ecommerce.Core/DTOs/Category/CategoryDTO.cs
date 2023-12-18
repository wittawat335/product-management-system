
namespace Ecommerce.Core.DTOs.Category
{
    public class CategoryDTO
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Status { get; set; }
        public string CreateDate { get; set; }
        public bool Selected { get;set; } = false;
    }
}
