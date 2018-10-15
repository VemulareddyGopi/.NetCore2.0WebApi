using ASP.NetCore.Models.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NetCore.Api.Helpers
{
    public static class JWTHelper
    {
        public static string BuildToken(User user, IConfiguration _config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
               claims,
               expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);
            token.Payload["id"] = user.Id;
            token.Payload["name"] = user.Name;
            token.Payload["email"] = user.Email;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
