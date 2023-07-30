using Microsoft.IdentityModel.Tokens;
using Ecommerce.Common.Helpers;
using Ecommerce.Common.KeysAndValues;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Common.JWT
{
    public static class TokenManger
    {
        #region Private Fields

        private static readonly string Secret = AppSettingHelper.GetJwtTokenSecret();

        #endregion Private Fields

        #region Private Methods

        private static ClaimsPrincipal GetPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            if (jwtToken == null)
                return null;
            byte[] key = Encoding.ASCII.GetBytes(Secret);
            var parameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
            return tokenHandler.ValidateToken(token,
                parameters, out _);
        }

        #endregion Private Methods

        #region Methods

        public static string GenerateToken(long id, AccountType type)
        {
            byte[] key = Encoding.ASCII.GetBytes(Secret);

            var securityKey = new SymmetricSecurityKey(key);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(TokenClaimKeys.Value, CryptoHelper.SymmetricEncryptString(AppSettingHelper.GetJwtValueSecret(), id.ToString())),
                    new Claim(TokenClaimKeys.Type, CryptoHelper.SymmetricEncryptString(AppSettingHelper.GetJwtValueSecret(), type.ToString()))
                }),

                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest)
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public static TokenModel ValidateToken(string token)
        {
            try
            {
                ClaimsPrincipal principal = GetPrincipal(token);
                if (principal == null)
                    return null;
                ClaimsIdentity identity = (ClaimsIdentity)principal.Identity;

                // Token values
                var id = long.Parse(CryptoHelper.SymmetricDecryptString(AppSettingHelper.GetJwtValueSecret(), identity.FindFirst(TokenClaimKeys.Value).Value));

                var type = EnumHelper.GetEnumByString<AccountType>(CryptoHelper.SymmetricDecryptString(AppSettingHelper.GetJwtValueSecret(), identity.FindFirst(TokenClaimKeys.Type).Value));

                var issuedAt = long.Parse(identity.FindFirst(TokenClaimKeys.IssuedAt).Value);

                var expiresAt = long.Parse(identity.FindFirst(TokenClaimKeys.ExpiresAt).Value);

                var notValidBefore = long.Parse(identity.FindFirst(TokenClaimKeys.NotValidBefore).Value);

                return new TokenModel(id, issuedAt, expiresAt, notValidBefore, type);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }

        #endregion Methods
    }
}