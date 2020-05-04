using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HanuEdmsApi.Helpers
{
    public static class JwtUtils
    {
        private const string SECRET = "hanuedms_api_thisisaverylongsecretkeyfromhanuedmsapistaystrong^^";
        private const long VALID_DURATION_SECONDS = 86400;
        private const string ISSUER = "HANU_EDMS";

        private static List<string> JwtBlacklist = new List<string>();

        public static string GenerateJWT(string username, string secret = SECRET, long validDuration = VALID_DURATION_SECONDS, string issuer = ISSUER)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var secToken = new JwtSecurityToken(
                signingCredentials: credentials,
                issuer: issuer,
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, username)
                },
                expires: DateTime.UtcNow.AddSeconds(validDuration)
            );

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(secToken);
        }

        public static string ValidateJWT(string jwt)
        {
            if (JwtBlacklist.Contains(jwt)) return "";

            var tokenHandler = new JwtSecurityTokenHandler();
            var issuerSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET));
            var validationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidIssuer = ISSUER,
                IssuerSigningKey = issuerSecurityKey,
                ValidateAudience = false
            };

            IPrincipal principal = tokenHandler.ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);

            return principal != null ? principal.Identity.Name : "";
        }

        public static void InvalidateJWT(string jwt)
        {
            JwtBlacklist.Add(jwt);
        }
    }
}
