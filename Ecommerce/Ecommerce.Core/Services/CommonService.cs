using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.RepositoryContracts;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Core.Services
{
    public class CommonService : ICommonService
    {
        private readonly IGenericRepository<Position> _postRespository;
        private readonly IGenericRepository<Menu> _menuRespository;

        public CommonService(IGenericRepository<Position> postRespository, IGenericRepository<Menu> menuRespository)
        {
            _postRespository = postRespository;
            _menuRespository = menuRespository;
        }

        public string Decrypt(string text) // ใช้ถอดรหัส
        {

            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(text);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    text = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return text;
        }
        public string Encrypt(string text) // ใช้แปลงรหัส
        {

            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(text);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    text = Convert.ToBase64String(ms.ToArray());

                }
            }

            return text;
        }
        public string GetMenuDefault(string menuId)
        {

            var query = _menuRespository.Get(x => x.MenuId == menuId);
            string url = query != null ? query.Url : "";

            return url;
        }
        public string GetParameter(string code)
        {
            throw new NotImplementedException();
        }
        public string GetPositionName(string id)
        {
            var query = _postRespository.Get(x => x.PositionId == id);
            string position = query != null ? query.PositionName : "";

            return position;
        }
    }
}
