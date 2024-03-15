﻿using System;
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
            public const string No_Data = "ไม่มีข้อมูล";
            public const string Could_Not_Create = "Could not create";
            public const string No_Delete = "No Deleted";
            public const string DuplicateUser = "มีบัญชีนี้อยู่ในระบบแล้ว";
            public const string DuplicatePosition = "Position is Duplicate";
            public const string DuplicateData = "Date is Duplicate";
            public const string DuplicateId = "มีรหัสนี้อยู่ในระบบแล้ว";
            public const string DuplicateName = "มีชื่อนี้อยู่ในระบบแล้ว";
            public const string Cannot_Update_Data = "Cannot Update Data";
            public const string Cannot_Map_Data = "Cannot Map Data";
            public const string UserInActive = "บัญชีนี้ถูกระงับการใช้งาน";
            public const string SaveSuccessfully = "บันทึกขัอมูลเรียบร้อย";
            public const string InsertSuccessfully = "เพิ่มขัอมูลเรียบร้อย";
            public const string UpdateSuccessfully = "แก้ไขขัอมูลเรียบร้อย";
            public const string DeleteSuccessfully = "ลบขัอมูลเรียบร้อย";
            public const string MappingError = "Data can't mapping";
            public const string PasswordDecryptError = "Password can't decrypt";
        }
        public struct JsTreeConfig
        {
            public const string TextAdd = "เพิ่มข้อมูล";
            public const string TextEdit = "แก้ไขข้อมูล";
            public const string TextView = "ดูข้อมูล";

            public const string IconDefault = "fas fa-folder";
            public const string IconAdd = "fas fa-plus";
            public const string IconEdit = "fas fa-pen";
            public const string IconView = "fas fa-eye";
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

        public struct Position
        {
            public static string Administrator = "Administrator";
            public static string User = "User";
            public static string Customer = "Customer";
        }
    }
}
