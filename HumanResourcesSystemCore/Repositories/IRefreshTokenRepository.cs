using HumanResourcesSystemCore.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<string> Generate(string userId);
        bool Validate(RefreshToken refreshToken);
        Task Remove(string token);
        Task<RefreshToken> GetByTokenAsync(string refreshToken);
        Task<RefreshToken> GetByUserIdAsync(string userId);
        Task Update(RefreshToken refreshToken);
        void RemoveExpiredRefreshTokens(string userId);
    }
}
