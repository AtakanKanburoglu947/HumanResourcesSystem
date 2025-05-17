using HumanResourcesSystemCore.AuthDtos;
using HumanResourcesSystemCore.AuthModels;
using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemCore.Repositories
{
    public interface IAuthRepository
    {
        Task Register(Register register);
        Task Login(Login login);
        void Logout();
        Task ChangePassword(string newPassword, string email);
        Task<AuthDto> RefreshToken(string refreshToken);
        bool ValidateToken(string token);
        void RemoveExpiredRefreshTokens(string userId);
        AccountDto GetAccountDetailsFromToken();
        Task<IQueryable<User>> UsersWithRole(string roleName);
        Task<bool> HasRole(string roleName, User user);

    }
}
