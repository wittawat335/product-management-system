namespace Ecommerce.Web.Commons
{
    public class Constants
    {
        public struct AppSettings
        {
            public const string BaseApiUrl = "AppSettings:BaseApiUrl";
            public const string DockerBaseApiUrl = "AppSettings:DockerBaseApiUrl";
        }
        public struct SelectOption
        {
            public static string SelectAll = "--- Select All ---";
            public static string SelectOne = "--- กรุณาเลือก ---";
        }
        public struct SessionKey
        {
            public const string sessionLogin = "sessionLogin";
            public const string permission = "listPermission";
            public const string accessToken = "JwtToken";
        }
        public struct UrlAction
        {
            public const string Login = "~/Authen/Login";
            public const string Register = "~/Authen/Register";
            public struct Product
            {
                public const string GetList = "~/Product/GetList";
                public const string PopUpDialog = "~/Product/_PopUpDialog";
                public const string Save = "~/Product/Save";
                public const string SaveImage = "~/Product/SaveImage";
                public const string Delete = "~/Product/Delete";
                public const string select2Product = "~/Product/select2Product";
                public const string select2Category = "~/Product/select2Category";
            }

            public struct Category
            {
                public const string GetList = "~/Category/GetList";
                public const string PopUpDialog = "~/Category/_PopUpDialog";
                public const string Save = "~/Category/Save";
                public const string Delete = "~/Category/Delete";
            }

            public struct Contract
            {
                public const string SendEmail = "~/Contract/SendEmail";
            }

            public struct Position
            {
                public const string GetList = "~/Position/GetListPosition";
                public const string PopUpMenuPosition = "~/Position/_PopUpMenuPosition";
                public const string PopUpDialog = "~/Position/_PopUpPosition";
                public const string Save = "~/Position/SavePosition";
                public const string Delete = "~/Position/DeletePosition";
                //--------------JStree----------------
                public const string GetJsTree = "~/Position/JsTree";
                public const string SetPermission = "~/Position/SetPermission";
                public const string SavePermission = "~/Position/SavePermission";
            }

            public struct User
            {
                public const string GetList = "~/User/GetList";
                public const string PopUpDialog = "~/User/_PopUpDialog";
                public const string Save = "~/User/Save";
                public const string Delete = "~/User/Delete";
                public const string select2userName = "~/User/select2UserName";
                public const string select2positionName = "~/User/select2Position";
            }

            public struct Master
            {
                public const string GetList = "~/Master/GetList";
                public const string Save = "~/Master/Save";
                public const string Delete = "~/Master/Delete";
            }
            public struct Menu
            {
                public const string GetList = "~/Menu/GetList";
                public const string GetListRoleMenu = "~/Menu/GetListRoleMenu";
                public const string Save = "~/Menu/Save";
                public const string SaveRoleMenu = "~/Menu/SaveRoleMenu";
                public const string Delete = "~/Menu/Delete";
                public const string DeleteRoleMenu = "~/Menu/DeleteRoleMenu";
                public const string GetListRoleMenuByRole = "~/Menu/GetListRoleMenuByRole";
            }

        }
        public struct MessageError
        {
            public const string CallAPI = "Error calling API";
        }

        public struct ddlValue
        {
            public struct MasterType
            {
                public const string Code = "MASTER_TYPE";
                public const string Text = "MASTER_TYPE";
            }
            public struct Category
            {
                public const string Code = "Id";
                public const string Text = "Name";
            }
            public struct StatusList
            {
                public const string Code = "CODE";
                public const string Text = "TEXT";
            }
        }

        public struct Action
        {
            public const string Add = "Add";
            public const string Update = "Update";
            public const string Delete = "Delete";
            public const string View = "View";
        }

        public struct Menu
        {
            public struct User
            {
                public const string Id = "MN01";
                public struct ActId
                {
                    public const string New = "ACT001";
                    public const string Update = "ACT002";
                    public const string View = "ACT002";
                    public const string Delete = "ACT004";
                }
            }

            public struct Category
            {
                public const string Id = "MN04";
                public struct ActId
                {
                    public const string New = "ACT009";
                    public const string Update = "ACT010";
                    public const string View = "ACT011";
                    public const string Delete = "ACT012";
                }
            }
        }
    }
}
