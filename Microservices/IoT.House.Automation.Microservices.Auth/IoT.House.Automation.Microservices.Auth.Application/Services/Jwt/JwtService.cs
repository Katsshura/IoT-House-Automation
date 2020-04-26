using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IoT.House.Automation.Microservices.Auth.Application.Entities.Config;
using IoT.House.Automation.Microservices.Auth.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace IoT.House.Automation.Microservices.Auth.Application.Services.Jwt
{
    public class JwtService : IJwt
    {
        private readonly JwtConfig _jwtConfig;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public JwtService(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public string CreateToken(string username)
        {
            var key = Convert.FromBase64String(_jwtConfig.Secret);
            var securityKey = new SymmetricSecurityKey(key);
            var descriptor = GetDescriptor(username, securityKey);
            var token = _tokenHandler.CreateJwtSecurityToken(descriptor);
            return _tokenHandler.WriteToken(token);
        }

        public string GetClaimValue(string token)
        {
            var claimsPrincipal = GetClaimsPrincipal(token);

            if (claimsPrincipal == null) return null;

            var identity = (ClaimsIdentity) claimsPrincipal.Identity;
            var claim = identity.FindFirst(ClaimTypes.Name);
            return claim.Value;
        }

        public bool IsTokenValid(string token)
        {
            var jToken = (JwtSecurityToken) _tokenHandler.ReadToken(token);
            return jToken?.ValidTo >= DateTime.UtcNow;
        }

        private SecurityTokenDescriptor GetDescriptor(string username, SymmetricSecurityKey securityKey)
        {
            return new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new[] { new Claim(ClaimTypes.Name, username) }),
                Expires = JwtExpirationExchanger.ExchangeExpirationTime(_jwtConfig),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
        }

        private ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            var jwtToken = (JwtSecurityToken)_tokenHandler.ReadToken(token);

            if (jwtToken == null) return null;

            var key = Convert.FromBase64String(_jwtConfig.Secret);
            var tokenParameters = GetTokenParameters(key);
            var claim = _tokenHandler.ValidateToken(token, tokenParameters, out _);
            return claim;
        }

        private TokenValidationParameters GetTokenParameters(byte[] key)
        {
            return new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        }
    }
}
