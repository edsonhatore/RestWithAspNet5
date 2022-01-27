using Microsoft.IdentityModel.Tokens;
using RestWithAspNet5.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RestWithAspNet5.Services.Implementation
{
    public class TokenService : ITokenService
    {

        private TokenConfiguration _configuration;
        private ClaimsPrincipal principal;

        public TokenService(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var Options = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_configuration.Minutes), 
                signingCredentials: signinCredentials);

            string tokenString= new  JwtSecurityTokenHandler().WriteToken(Options);
            return tokenString;


        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using( var rng = RandomNumberGenerator.Create() )
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);

            }
            
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {

            var tokenValidationparameters = new TokenValidationParameters
            { ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
                ValidateLifetime = false


            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationparameters , out securityToken);
            var JwtSecurityToken = securityToken as JwtSecurityToken;

            if (JwtSecurityToken ==null  || 
                !JwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
               throw new SecurityTokenException("Invalid Token");    
                                      
         
            return principal;
            
        }
    }
}
