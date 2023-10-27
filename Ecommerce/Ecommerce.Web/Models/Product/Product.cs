using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.Models.Product
{
    public class Product
    {
        [Display(Name = "รหัสสินค้า")]
        public string ProductId { get; set; }
        [Display(Name = "ชื่อสินค้า")]
        public string ProductName { get; set; }
        [Display(Name = "ประเภทสินค้า")]
        public string CategoryId { get; set; }
        [Display(Name = "ประเภทสินค้า")]
        public string CategoryName { get; set; }
        [Display(Name = "รูปภาพสินค้า")]
        public string Image { get; set; }
        //public IFormFile ImageFile { get; set; }
        [Display(Name = "จำนวนสินค้าที่มี (ชิ้น)")]
        public string Stock { get; set; }
        [Display(Name = "ราคาสินค้า (บาท)")]
        public string Price { get; set; }
        [Display(Name = "รายละเอียดสินค้า")]
        public string Description { get; set; }
        [Display(Name = "สถานะ")]
        public string Status { get; set; }
        [Display(Name = "วันที่บันทึก")]
        public string CreateDate { get; set; }
    }
}
