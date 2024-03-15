using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.Models.Email
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        [Display(Name = "ชื่อผู้ติดต่อ")]
        public string Name { get; set; }
        [Display(Name = "อีเมลล์ติดต่อกลับ")]
        public string ReturnEmail { get; set; }
        [Display(Name = "หัวช้อเรื่อง")]
        public string Subject { get; set; }
        public string Message { get; set; }
        [Display(Name = "ข้อความ")]
        public string Body { get; set; }
    }
}
