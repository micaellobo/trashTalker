using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using TrashTalker.Helpers;
using TrashTalker.Models;
using static TrashTalker.Config;

namespace TrashTalker.Services
{
    /// <summary>
    /// Class that allows manage the creation of JWT Tokens
    /// </summary>
    public static class TokenService
    {
        /// <summary>
        /// Generates an token for an specific <see cref="User"/>
        /// </summary>
        /// <param name="user"><see cref="User"/> to generate the token</param>
        /// <returns> A <see cref="String"/> representing the token for the user</returns>
        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SECRET);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                    new Claim(ClaimTypes.Role, user.role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(TOKEN_DURATION),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}