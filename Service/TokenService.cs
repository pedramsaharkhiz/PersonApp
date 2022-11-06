using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Interface;
using API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            this._config = config;

        }
        public string CreateToken(User user)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor=new SecurityTokenDescriptor{
                Subject=new ClaimsIdentity(Claims),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials=creds
            };
            var TokenHandler=new JwtSecurityTokenHandler();
            var token=TokenHandler.CreateToken(tokenDescriptor);
            return TokenHandler.WriteToken(token);
        }
    }
}