using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyLoginRegistration.Models;

namespace MyLoginRegistration.Security
{
    public class JwtService
    {
        public static string GenerateToken(UserAccount user)
        {
            var secretKey = ConfigurationManager.AppSettings["JwtSecret"];
            var key = Encoding.UTF8.GetBytes(secretKey);

            var claims = new[]
            {
                 new Claim("UserID", user.UserID.ToString()),
                 new Claim(ClaimTypes.Name, user.Username),
                 new Claim(ClaimTypes.Role, user.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["JwtExpiryMinutes"])),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}