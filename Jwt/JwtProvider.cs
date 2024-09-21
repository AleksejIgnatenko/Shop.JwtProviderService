﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shop.JwtProviderService.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop.JwtProviderService.Jwt
{
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;

        public string GenerateToken(JwtRequest jwtRequest)
        {
            Claim[] claims = [new("userId", jwtRequest.Id.ToString()),
                             new("role", jwtRequest.Role.ToString())];

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials
                //expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public Guid GetUserIdFromToken(string jwtToken)
        {
            // Разбор JWT-токена для извлечения идентификатора пользователя
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(jwtToken);
            var userId = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception();
            }

            return Guid.Parse(userId);
        }
    }
}
