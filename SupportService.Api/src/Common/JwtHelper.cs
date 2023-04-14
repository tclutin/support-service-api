using Microsoft.IdentityModel.Tokens;

namespace SupportService.Api.src.Common
{
    public static class JwtHelper
    {
        public static bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }

    }
}
