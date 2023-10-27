using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.Models.User
{
    public class User
    {
        public string UserId { get; set; }
        [Display(Name = "บัญชีผู้ใช้")]
        public string Username { get; set; }
        [Display(Name = "รหัสผ่าน")]
        public string Password { get; set; }
        [Display(Name = "ชื่อ-สกุล")]
        public string FullName { get; set; }
        [Display(Name = "เบอร์โทรศัพท์")]
        public string PhoneNumber { get; set; }
        [Display(Name = "อีเมลล์")]
        public string Email { get; set; }
        [Display(Name = "ตำแหน่ง")]
        public string PositionId { get; set; }
        [Display(Name = "ชื่อตำแหน่ง")]
        public string PositionName { get; set; }
        [Display(Name = "ที่อยู่")]
        public string Address { get; set; }
        [Display(Name = "สถานะ")]
        public string Status { get; set; }
        [Display(Name = "วันที่บันทึก")]
        public string CreateDate { get; set; }
    }
}
