using System.Security.Cryptography;
using System.Text;

namespace DeliveryEat_vue1.Server.Model
{
    public static class EncryptionHash
    {
        public static string Encrypt(string data)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(hash);

        }
    }
}
