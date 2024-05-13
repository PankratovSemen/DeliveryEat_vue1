using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DeliveryEat_vue1.Server.Model
{
    public class AuthOptions
    {
        public const string ISSUER = "DeliveryEatSemen"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "DeliveryEatSemen_gjeooqseeffff2242422eddd2e222";   // ключ для шифрации
        public const int LIFETIME = 5; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
