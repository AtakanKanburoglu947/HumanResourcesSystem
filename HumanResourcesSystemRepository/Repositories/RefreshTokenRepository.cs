using HumanResourcesSystemCore.AuthModels;
using HumanResourcesSystemCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemRepository.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _appDbContext;
        public RefreshTokenRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<string> Generate()
        {
            string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            RefreshToken refreshToken = new RefreshToken()
            {
                Token = token,
                ExpireDate = DateTime.UtcNow.AddDays(7),   
            };
           
            await _appDbContext.RefreshTokens.AddAsync(refreshToken);
            return token;
        }

        public async Task Validate(string refreshToken)
        {
            var refreshTokenInDatabase = await _appDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);
            if (refreshTokenInDatabase == null || refreshTokenInDatabase.ExpireDate < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }
        }
    }
}
