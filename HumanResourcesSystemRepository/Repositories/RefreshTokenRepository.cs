using HumanResourcesSystemCore;
using HumanResourcesSystemCore.AuthModels;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Repositories;
using HumanResourcesSystemRepository.Migrations;
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
        public async Task<string> Generate(string userId)
        {
          
            string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            RefreshToken refreshToken = new RefreshToken()
            {
                Token = token,
                UserId = userId,
                ExpireDate = DateTime.UtcNow.AddDays(7),
            };

            await _appDbContext.RefreshTokens.AddAsync(refreshToken);
            await _appDbContext.SaveChangesAsync();
            return token;
        }

        public void RemoveExpiredRefreshTokens(string userId)
        {
            var oldTokens = _appDbContext.RefreshTokens.Where(x => x.UserId == userId).Where(x=>x.ExpireDate < DateTime.UtcNow);
            _appDbContext.RefreshTokens.RemoveRange(oldTokens);
        }
        public async Task<RefreshToken?> GetByTokenAsync(string refreshToken)
        {
            return await _appDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);
        }

        public async Task<RefreshToken?> GetByUserIdAsync(string userId)
        {
            return await _appDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task Remove(string token)
        {
            var refreshTokenInDatabase = await _appDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
            if (refreshTokenInDatabase != null)
            {
                _appDbContext.RefreshTokens.Remove(refreshTokenInDatabase);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }
        }

        public async Task Update(RefreshToken refreshToken)
        {
            _appDbContext.RefreshTokens.Update(refreshToken);  
            await _appDbContext.SaveChangesAsync();
        }

        public bool Validate(RefreshToken refreshToken)
        {
            if (refreshToken == null || refreshToken.ExpireDate < DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }
    }
}
