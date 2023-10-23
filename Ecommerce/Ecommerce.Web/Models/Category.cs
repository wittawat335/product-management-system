using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.Models
{
    public class Category
    {
        [Display(Name = "รหัสประเภทสินค้า")]
        public string CategoryId { get; set; }
        [Display(Name = "ชื่อประเภทสินค้า")]
        public string CategoryName { get; set; }
        [Display(Name = "สถานะ")]
        public string Status { get; set; }
        [Display(Name = "วันที่บันทึก")]
        public string CreateDate { get; set; }
    }
}
