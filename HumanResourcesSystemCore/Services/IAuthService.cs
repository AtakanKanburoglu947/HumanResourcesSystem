using HumanResourcesSystemCore.AuthDtos;
using HumanResourcesSystemCore.AuthModels;
using HumanResourcesSystemCore.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Services
{
    public interface IAuthService
    {
        Task Register(Register register);
        Task Login(Login login);
        void Logout();
        Task ChangePassword(string newPassword, string email);
        bool ValidateToken(string token);
        Task<AuthDto> RefreshToken(string refreshToken);
        Task RemoveExpiredRefreshTokens(string userId);
        AccountDto GetAccountDetailsFromToken();


    }
}
