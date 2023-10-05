using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        [Display(Name = "ชื่อสินค้า")]
        public string ProductName { get; set; }
        [Display(Name = "ประเภทสินค้า")]
        public string CategoryId { get; set; }
        [Display(Name = "ชื่อสินค้า")]
        public string CategoryName { get; set; }
        [Display(Name = "รูปภาพสินค้า")]
        public string Image { get; set; }
        [Display(Name = "จำนวนสินค้าที่มี")]
        public int Stock { get; set; }
        [Display(Name = "ราคาสินค้า")]
        public string Price { get; set; }
        [Display(Name = "รายละเอียดสินค้า")]
        public string Description { get; set; }
        [Display(Name = "สถานะสินค้า")]
        public string Status { get; set; }
        public string CreateDate { get; set; }
    }
}
