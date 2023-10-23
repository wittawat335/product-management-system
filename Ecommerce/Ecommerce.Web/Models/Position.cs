using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.Models
{
    public class Position
    {
        [Display(Name = "รหัสตำแหน่ง")]
        public string PositionId { get; set; }
        [Display(Name = "ชื่อตำแหน่ง")]
        public string PositionName { get; set; }
        [Display(Name = "หน้าแรก")]
        public string MenuDefault { get; set; }
        [Display(Name = "หน้าแรก")]
        public string MenuDefaultName { get; set; }
        [Display(Name = "สถานะ")]
        public string Status { get; set; }
        [Display(Name = "วันที่บันทึก")]
        public string CreateDate { get; set; }
    }
}
