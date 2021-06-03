using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace clientes_api.Services
{
    public static class TokenService
    {
        private static string Secret = "ASD0A09S809VXCVLXCVJKÇ2423L54WI9fsdd223vdvxcVXCZO889XZZZZXCVVPE6K45PO" ;
        public static string GenerateToken(String nomeUsuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, nomeUsuario)
                }),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static bool Validate(string token)
        {
            token = token.Replace("Bearer ", String.Empty);

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    IssuerSigningKey = key

                }, out SecurityToken validatedToken);
            }
            catch (Exception)
            {                
                return false;
            }

            return true;
        }
    }//end of class
}//end of namespace