using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.Models
{
    public class Category
    {
        public string CategoryId { get; set; }
        [Display(Name = "ชื่อสินค้า")]
        public string CategoryName { get; set; }
        [Display(Name = "สถานะสินค้า")]
        public string Status { get; set; }
        [Display(Name = "วันที่บันทึก")]
        public string CreateDate { get; set; }
    }
}
