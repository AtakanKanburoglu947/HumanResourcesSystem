using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemRepository.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ICookieRepository _cookieRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenRepository(IConfiguration configuration, ICookieRepository cookieRepository, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _cookieRepository = cookieRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public AccountDto Decode()
        {
            string token = _cookieRepository.Get(_httpContextAccessor.HttpContext.Request,"token");
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = handler.ReadJwtToken(token);

            string? userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            string? userName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            string? userEmail = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            return new AccountDto()
            {
                Id = userId,
                Email = userEmail,
                Name = userName
            };

        }

        public string Generate(IEnumerable<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expiration = DateTime.UtcNow.AddMinutes(10);
            JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    expires: expiration,
                    signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public bool Validate(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            try
            {
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["JWT:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
