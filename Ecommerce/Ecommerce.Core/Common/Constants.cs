using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Common
{
    public class Constants
    {
        public struct AppSettings
        {
            public const string Client_URL = "AppSettings:Client_URL";
            public const string CorsPolicy = "AppSettings:CorsPolicy";
        }

        public struct StatusMessage
        {
            public const string RegisterSuccess = "ลงทะเบียนสำเร็จ";
            public const string LoginSuccess = "เข้าสู่ระบบสำเร็จ";
            public const string InvaildPassword = "รหัสผ่านไม่ถูกต้อง";
            public const string NotFoundUser = "ไม่มีบัญชีผู้ใช้นี้";
            public const string Success = "OK";
            public const string No_Data = "No Data";
            public const string Could_Not_Create = "Could not create";
            public const string No_Delete = "No Deleted";
            public const string DuplicateUser = "Username is Duplicate";
            public const string DuplicatePosition = "Position is Duplicate";
            public const string Cannot_Update_Data = "Cannot Update Data";
            public const string Cannot_Map_Data = "Cannot Map Data";
            public const string UserInActive = "บัญชีนี้ถูกระงับการใช้งาน";
            public const string AddSuccessfully = "เพิ่มขัอมูลเรียบร้อย";
            public const string UpdateSuccessfully = "แก้ไขขัอมูลเรียบร้อย";
            public const string DeleteSuccessfully = "ลบขัอมูลเรียบร้อย";
        }
        public struct Status
        {
            public const bool True = true;
            public const bool False = false;
            public static string Active = "A";
            public static string ActiveText = "Active";
            public static string Inactive = "I";
            public static string InactiveText = "Inactive";
        }

        public struct PositionId
        {
            public static string Administrator = "08FE48BE-AC81-4983-9A0A-4EEB2972C947";
            public static string User = "A06B5B8E-F94A-4DBA-A82E-D70F439548CF";
            public static string Customer = "5FAB735A-A4CB-4EF3-BFD2-1FFC3BC37821";
        }
    }
}
